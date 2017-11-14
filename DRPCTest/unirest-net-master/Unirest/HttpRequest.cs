using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using unirest_net.http;
// ReSharper disable UnusedMethodReturnValue.Global

namespace unirest_net.request
{
    public class HttpRequest
    {
        private bool _hasFields;
        private bool _hasExplicitBody;
        private int _fileCount;
        
        /// <summary>
        /// Connection timeout
        /// </summary>
        public TimeSpan Timeout { get; set; } = TimeSpan.MaxValue;

        public NetworkCredential NetworkCredentials { get; protected set; }

        protected Func<HttpRequestMessage, bool> InternalFilter;
        public Func<HttpRequestMessage, bool> Filter
        {
            get => InternalFilter;
            set
            {
                if (InternalFilter != null)
                {
                    throw new InvalidOperationException("Processing filter is already set.");
                }

                InternalFilter = value;
            }
        }

        public virtual Uri Url { get; }

        public Uri Uri => Url;

        public HttpMethod HttpMethod { get; }

        public HeadersDict Headers { get; } = new HeadersDict();

        /// <summary>
        /// Shortcut to <see cref="SetFields"/> or individual <see cref="SetField(string,object)"/> calls
        /// (use object initializer)
        /// </summary>
        public FieldsDict Fields { get; }

        /// <summary>
        /// Shortcut to <see cref="SetFields"/> with SerializeObject
        /// (use object initializer)
        /// </summary>
        public FieldsDictJson JsonFields { get; }

        /// <summary>
        /// Shortcut to <see cref="SetField(Stream)"/>
        /// </summary>
        public Stream Field
        {
            set => SetField(value);
        }
        
        /// <summary>
        /// Shortcut for <see cref="Headers"/>.<see cref="HeadersDict.UserAgent"/>
        /// </summary>
        public string UserAgent {
            get => Headers["User-Agent"];
            set => Headers["User-Agent"] = value;
        }

        private readonly Lazy<HttpContent> _body = new Lazy<HttpContent>(() => new MultipartFormDataContent());

        public HttpContent Body { get; set; }

        /// <summary>
        /// Shortcut for <see cref="SetBody(string)"/>
        /// </summary>
        public string BodyString
        {
            set => SetBody(value);
        }
        
        /// <summary>
        /// Shortcut for <see cref="SetBody(string)"/> with parameter JsonConvert-ed
        /// </summary>
        public object BodyJson
        {
            set => SetBody(JsonConvert.SerializeObject(value));
        }

        /// <summary>
        /// Shortcut for <see cref="SetBody&lt;T>"/>
        /// </summary>
        public object TypedBody
        {
            set => SetBody(value);
        }

        /// <summary>
        /// Shortcut for <see cref="BasicAuth"/>
        /// </summary>
        /// <exception cref="InvalidOperationException">If authentication credentials have already been set.</exception>
        public (string, string) Auth
        {
            set {
                if (NetworkCredentials != null)
                {
                    throw new InvalidOperationException("Basic authentication credentials are already set.");
                }
    
                NetworkCredentials = new NetworkCredential(value.Item1, value.Item2);
            }
        }

        // Should add overload that takes URL object
        public HttpRequest(HttpMethod method, string url)
        {
            Fields = new FieldsDict(this);
            JsonFields = new FieldsDictJson(this);
            Body = _body.Value;

            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var locurl))
            {
                if (!(locurl.IsAbsoluteUri && (locurl.Scheme == "http" || locurl.Scheme == "https")) || !locurl.IsAbsoluteUri)
                {
                    throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S URL");
                }
            }
            else
            {
                throw new ArgumentException("The url passed to the HttpMethod constructor is not a valid HTTP/S URL");
            }

            Url = locurl;
            HttpMethod = method;
        }

        public HttpRequest Header(string name, object value)
        {
            if (value != null)
                Headers.Add(name, value.ToString());

            return this;
        }
        
        public HttpRequest SetHeaders(Dictionary<string, object> headers)
        {
            if (headers == null) return this;
            foreach (var header in headers)
            {
                if(header.Value != null)
                    Headers.Add(header.Key, header.Value.ToString());
            }

            return this;
        }

        public HttpRequest SetField(string name, object value)
        {
            if (value is byte[] || value is IEnumerable<byte>)
            {
                SetField(name, value as byte[]);
                return this;
            }
            
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasExplicitBody)
            {
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            }

            if (value == null)
                return this;

            if (!(Body is MultipartFormDataContent))
                Body = new MultipartFormDataContent();
            
            ((MultipartFormDataContent) Body).Add(new StringContent(value.ToString()), name);

            _hasFields = true;           

            return this;
        }

        public HttpRequest SetField(string name, byte[] data)
        {
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasExplicitBody)
            {
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            }

            if (data == null)
                return this;

            //    here you can specify boundary if you need---^
            var imageContent = new ByteArrayContent(data);
            imageContent.Headers.ContentType =
                MediaTypeHeaderValue.Parse("image/jpeg");

            if (!(Body is MultipartFormDataContent))
                Body = new MultipartFormDataContent();
            
            ((MultipartFormDataContent) Body).Add(imageContent, name, "image.jpg");

            _hasFields = true;
            return this;
        }

        public HttpRequest BasicAuth(string userName, string passWord)
        {
            if (NetworkCredentials != null)
            {
                throw new InvalidOperationException("Basic authentication credentials are already set.");
            }

            NetworkCredentials = new NetworkCredential(userName, passWord);
            return this;
        }

        /// <summary>
        /// Set a delegate to a message filter. This is particularly useful for using external
        /// authentication libraries such as uniauth (https://github.com/zeeshanejaz/uniauth-net)
        /// </summary>
        /// <param name="filter">Filter accepting HttpRequestMessage and returning bool</param>
        /// <returns>updated reference</returns>
        public HttpRequest SetFilter(Func<HttpRequestMessage, bool> filter)
        {
            Filter = filter;
            return this;
        }

        public HttpRequest SetField(Stream value)
        {
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasExplicitBody)
            {
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            }

            if (value == null)
                return this;

            if (!(Body is MultipartFormDataContent))
                Body = new MultipartFormDataContent();

            ((MultipartFormDataContent) Body).Add(new StreamContent(value));

            _hasFields = true;
            return this;
        }

        public HttpRequest SetField(string name, Stream fileStream, string filename=null, string contentType=null)
        {
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasExplicitBody)
            {
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            }

            if (null == fileStream || !fileStream.CanRead)
                return this;
         
            if (!(Body is MultipartFormDataContent))
                Body = new MultipartFormDataContent();
                
            var fileStreamContent = new StreamContent(fileStream);
            if (contentType != null)
                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            var fname = $"file{++_fileCount}";
            ((MultipartFormDataContent) Body).Add(fileStreamContent, name, filename ?? fname);

            _hasFields = true;
            return this;
        }

        public HttpRequest SetFields(Dictionary<string, object> parameters)
        {
            if (parameters == null)
                return this;

            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasExplicitBody)
            {
                throw new InvalidOperationException("Can't add fields to a request with an explicit body");
            }

            if (!(Body is MultipartFormDataContent))
                Body = new MultipartFormDataContent();

            ((MultipartFormDataContent) Body).Add(
                new FormUrlEncodedContent(parameters
                    .Where(kv => IsPrimitiveType(kv.Value))
                    .Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value.ToString()))));

            foreach (var stream in parameters.Where(kv => kv.Value is Stream).Select(kv => kv.Value))
            {
                ((MultipartFormDataContent) Body).Add(new StreamContent(stream as Stream));
            }

            _hasFields = true;
            return this;
        }

        public HttpRequest SetBody(string body)
        {
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasFields)
            {
                throw new InvalidOperationException("Can't add explicit body to request with fields");
            }

            if (body == null)
                return this;

            Body = new StringContent(body);
            _hasExplicitBody = true;
            return this;
        }

        public HttpRequest SetBody<T>(T body)
        {
            if (HttpMethod == HttpMethod.Get || HttpMethod == HttpMethod.Head || HttpMethod == HttpMethod.Trace)
            {
                throw new InvalidOperationException($"Can't add body to {HttpMethod} request.");
            }

            if (_hasFields)
            {
                throw new InvalidOperationException("Can't add explicit body to request with fields");
            }

            if (body == null)
                return this;

            if (body is Stream)
            {
                var inputStream = body as Stream;
                if (!inputStream.CanRead)
                    throw new ArgumentException("Excepting a readable stream");

                var reader = new StreamReader(inputStream);
                var fileContent = reader.ReadToEnd();
                Body = new MultipartFormDataContent { new StringContent(fileContent) };
            }
            else
            {
                Body = new StringContent(JsonConvert.SerializeObject(body));
            }

            _hasExplicitBody = true;
            return this;
        }

        public HttpResponse<string> AsString()
        {
            return HttpClientHelper.Request<string>(this);
        }

        public Task<HttpResponse<string>> AsStringAsync()
        {
            return HttpClientHelper.RequestAsync<string>(this);
        }

        public HttpResponse<Stream> AsBinary()
        {
            return HttpClientHelper.RequestStream<Stream>(this);
        }

        public Task<HttpResponse<Stream>> AsBinaryAsync()
        {
            return HttpClientHelper.RequestStreamAsync<Stream>(this);
        }

        public HttpResponse<T> AsJson<T>()
        {
            return HttpClientHelper.Request<T>(this);
        }

        public Task<HttpResponse<T>> AsJsonAsync<T>()
        {
            return HttpClientHelper.RequestAsync<T>(this);
        }
        public HttpResponse<T> As<T>(Func<Stream,T> genFunc)
        {
            var stream = HttpClientHelper.RequestStream<Stream>(this);
            
            return new HttpResponse<T>(stream.Code, stream.Headers, genFunc(stream.Body), stream.Raw);
        }

        public async Task<HttpResponse<T>> AsAsync<T>(Func<Stream,T> genFunc)
        {
            var stream = await HttpClientHelper.RequestStreamAsync<Stream>(this);
            
            return new HttpResponse<T>(stream.Code, stream.Headers, genFunc(stream.Body), stream.Raw);
        }
        
        private static bool IsPrimitiveType(object obj)
        {
            if (obj == null)
                return false;

            return obj is string || obj is bool || obj is byte || obj is sbyte
                   || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong
                   || obj is IntPtr || obj is UIntPtr || obj is char || obj is double || obj is float;
        }

    }

    public class FieldsDict : Dictionary<string, object>
    {
        private HttpRequest Request { get; set; }
        
        public FieldsDict(HttpRequest mast)
        {
            Request = mast;
        }

        public new object this[string key]
        {
            get => base[key];
            set
            {
                Request.SetField(key, value);
                base[key] = value;
            }
        }
    }

    public class FieldsDictJson : Dictionary<string, object>
    {
        private HttpRequest Request { get; set; }
        
        public FieldsDictJson(HttpRequest mast)
        {
            Request = mast;
        }

        public new object this[string key]
        {
            get => base[key];
            set
            {
                Request.SetField(key, value);
                base[key] = value;
            }
        }
    }

    public class HeadersDict : Dictionary<string, string>
    {
        #region Header properties
        public string Accept {
            get => this["Accept"];
            set => this["Accept"] = value;
        }
        public string AcceptCharset {
            get => this["Accept-Charset"];
            set => this["Accept-Charset"] = value;
        }
        public string AcceptEncoding {
            get => this["Accept-Encoding"];
            set => this["Accept-Encoding"] = value;
        }
        public string AcceptLanguage {
            get => this["Accept-Language"];
            set => this["Accept-Language"] = value;
        }
        public string AcceptDatetime {
            get => this["Accept-Datetime"];
            set => this["Accept-Datetime"] = value;
        }
        public string Authorization {
            get => this["Authorization"];
            set => this["Authorization"] = value;
        }
        public string CacheControl {
            get => this["Cache-Control"];
            set => this["Cache-Control"] = value;
        }
        public string Connection {
            get => this["Connection"];
            set => this["Connection"] = value;
        }
        public string Cookie {
            get => this["Cookie"];
            set => this["Cookie"] = value;
        }
        public string ContentLength {
            get => this["Content-Length"];
            set => this["Content-Length"] = value;
        }
        public string ContentMD5 {
            get => this["Content-MD5"];
            set => this["Content-MD5"] = value;
        }
        public string ContentType {
            get => this["Content-Type"];
            set => this["Content-Type"] = value;
        }
        public string Date {
            get => this["Date"];
            set => this["Date"] = value;
        }
        public string Expect {
            get => this["Expect"];
            set => this["Expect"] = value;
        }
        public string Forwarded {
            get => this["Forwarded"];
            set => this["Forwarded"] = value;
        }
        public string From {
            get => this["From"];
            set => this["From"] = value;
        }
        public string Host {
            get => this["Host"];
            set => this["Host"] = value;
        }
        public string IfMatch {
            get => this["If-Match"];
            set => this["If-Match"] = value;
        }
        public string IfModifiedSince {
            get => this["If-Modified-Since"];
            set => this["If-Modified-Since"] = value;
        }
        public string IfNoneMatch {
            get => this["If-None-Match"];
            set => this["If-None-Match"] = value;
        }
        public string IfRange {
            get => this["If-Range"];
            set => this["If-Range"] = value;
        }
        public string IfUnmodifiedSince {
            get => this["If-Unmodified-Since"];
            set => this["If-Unmodified-Since"] = value;
        }
        public string MaxForwards {
            get => this["Max-Forwards"];
            set => this["Max-Forwards"] = value;
        }
        public string Origin {
            get => this["Origin"];
            set => this["Origin"] = value;
        }
        public string Pragma {
            get => this["Pragma"];
            set => this["Pragma"] = value;
        }
        public string ProxyAuthorization {
            get => this["Proxy-Authorization"];
            set => this["Proxy-Authorization"] = value;
        }
        public string Range {
            get => this["Range"];
            set => this["Range"] = value;
        }
        public string Referer {
            get => this["Referer"];
            set => this["Referer"] = value;
        }
        public string TransferEncodings {
            get => this["TE"];
            set => this["TE"] = value;
        }
        public string UserAgent {
            get => this["User-Agent"];
            set => this["User-Agent"] = value;
        }
        public string Upgrade {
            get => this["Upgrade"];
            set => this["Upgrade"] = value;
        }
        public string Via {
            get => this["Via"];
            set => this["Via"] = value;
        }
        public string Warning {
            get => this["Warning"];
            set => this["Warning"] = value;
        }
        public string DoNotTrack {
            get => this["DNT"];
            set => this["DNT"] = value;
        }
        public string DNT {
            get => this["DNT"];
            set => this["DNT"] = value;
        }
        #endregion
        
        #if DEBUG
        public static void Btesta()
        {
            var b = new GetRequest("")
            {
                Fields =
                {
                    [""] = new byte[] { 0, 1, 2 },
                    [""] = null,
                },
                Headers =
                {
                    [""] = "",
                    DNT = "1",
                }
            }.AsBinary();
        }
        #endif
    }

//    public class HeadersDict : BaseWeirdList<(string, string)>, IDictionary<string, string>, IList
//    {
//        // this one actually needs to implement dictionary
//        private readonly IDictionary<string, string> _dict;
//
//        public HeadersDict()
//        {
//            _dict = new Dictionary<string, string>();
//        }
//
//        public object this[int index]
//        {
//            get => throw new ArgumentException("Can't get from a write-only list!");
//            set
//            {
//                if (value == null) throw new ArgumentNullException(nameof(value));
//
//                if (!(value is ValueTuple<string, string> tup))
//                    throw new ArgumentException($"Wrong setter value {value}, should be (string, string) not {value.GetType()}!");
//
//                _dict[tup.Item1] = tup.Item2;
//            }
//        }
//        
//        public void Add(KeyValuePair<string, string> item)
//        {
//            _dict.Add(item);
//        }
//
//        public bool Contains(KeyValuePair<string, string> item)
//        {
//            return _dict.Contains(item);
//        }
//
//        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
//        {
//            _dict.CopyTo(array, arrayIndex);
//        }
//
//        public bool Remove(KeyValuePair<string, string> item)
//        {
//            return _dict.Remove(item);
//        }
//
//        public new IEnumerator<KeyValuePair<string, string>> GetEnumerator()
//        {
//            return _dict.GetEnumerator();
//        }
//
//        public bool ContainsKey(string key)
//        {
//            return _dict.ContainsKey(key);
//        }
//
//        public void Add(string key, string value)
//        {
//            _dict.Add(key, value);
//        }
//
//        public bool Remove(string key)
//        {
//            return _dict.Remove(key);
//        }
//
//        public bool TryGetValue(string key, out string value)
//        {
//            return _dict.TryGetValue(key, out value);
//        }
//
//        string IDictionary<string, string>.this[string key]
//        {
//            get => _dict[key];
//            set => _dict[key] = value;
//        }
//
//        public ICollection<string> Keys => _dict.Keys;
//
//        public ICollection<string> Values => _dict.Values;
//    }
//
//    public class FieldDict
//    {
//        private readonly HttpRequest _master;
//
//        public FieldDict(HttpRequest master)
//        {
//            _master = master;
//        }
//
//        public object this[string index]
//        {
//            set => _set(index, value);
//        }
//
//        private void _set(string key, object value)
//        {
//            if (value is byte[] bs) _master.SetField(key, bs);
//            if (value is HttpField field) _master.SetField(field.Name ?? key, field.FileStream, field.Filename, field.ContentType);
//            if (value != null) _master.SetField(key, (object) value);
//        }
//        
//        public static void Atesta()
//        {
//            var a = new GetRequest("")
//            {
//                Fields =
//                {
//                    [""] = new byte[] { 0, 1, 2 },
//                    [""] = null,
//                },
//                Headers =
//                {
//                    [""] = "",
//                }
//            }.AsBinary();
//        }
//    }
//
//    internal struct HttpField
//    {
//        public readonly string Name;
//        public readonly Stream FileStream;
//        public readonly string Filename;
//        public readonly string ContentType;
//
//        public HttpField(string name, Stream fileStream, string filename = null, string contentType = null)
//        {
//            Name = name;
//            FileStream = fileStream;
//            Filename = filename;
//            ContentType = contentType;
//        }
//    }
//
//    public class BaseWeirdList<T> : IEnumerable
//    {
//        public int IndexOf(T item)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void Insert(int index, T item)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void Remove(object value)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void RemoveAt(int index)
//        {
//            throw new NotSupportedException();
//        }
//        
//        public IEnumerator<T> GetEnumerator()
//        {
//            throw new NotSupportedException();
//        }
//
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            throw new NotSupportedException();
//        }
//
//        public virtual void Add(T item)
//        {
//            Count++;
//        }
//
//        public virtual int Add(object value)
//        {
//            return Count++;
//        }
//
//        public bool Contains(object value)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void Clear()
//        {
//            throw new NotSupportedException();
//        }
//
//        public int IndexOf(object value)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void Insert(int index, object value)
//        {
//            throw new NotSupportedException();
//        }
//
//        public bool Contains(T item)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void CopyTo(T[] array, int arrayIndex)
//        {
//            throw new NotSupportedException();
//        }
//
//        public bool Remove(T item)
//        {
//            throw new NotSupportedException();
//        }
//
//        public void CopyTo(Array array, int index)
//        {
//            throw new NotSupportedException();
//        }
//
//        public int Count { get; private set; }
//
//        public object SyncRoot => throw new NotSupportedException();
//        public bool IsSynchronized => true;
//        public bool IsReadOnly => false;
//        public bool IsFixedSize => false;
//    }
//
//    public class WriteOnlyList<T> : BaseWeirdList<T>, IList
//    {
//        object IList.this[int index]
//        {
//            get => default(T);
//            set { }
//        }
//    }
//
//    public class StrictWriteOnlyList<T> : WriteOnlyList<T>, IList<T>, IList
//    {
//        object IList.this[int index]
//        {
//            get => throw new ArgumentException("Can't get from a write-only list!");
//            set { }
//        }
//
//        T IList<T>.this[int index]
//        {
//            get => throw new ArgumentException("Can't get from a write-only list!");
//            set { }
//        }
//    }
}
