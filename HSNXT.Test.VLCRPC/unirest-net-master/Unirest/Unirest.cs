using System;
using System.Net.Http;
using Fallk.Unirest.Net.Unirest;
using unirest_net.request;

namespace unirest_net.http
{
    public class Unirest
    {
        internal static readonly HttpMethod PatchMethod = new HttpMethod("PATCH");

        // Should add overload that takes URL object
        public static HttpRequest Get(string url)
        {
            return new HttpRequest(HttpMethod.Get, url);
        }

        public static HttpRequest Post(string url)
        {
            return new HttpRequest(HttpMethod.Post, url);
        }

        public static HttpRequest Delete(string url)
        {
            return new HttpRequest(HttpMethod.Delete, url);
        }

        public static HttpRequest Patch(string url)
        {
            return new HttpRequest(PatchMethod, url);
        }

        public static HttpRequest Put(string url)
        {
            return new HttpRequest(HttpMethod.Put, url);
        }

        public static HttpRequest Options(string url)
        {
            return new HttpRequest(HttpMethod.Options, url);
        }

        public static HttpRequest Head(string url)
        {
            return new HttpRequest(HttpMethod.Head, url);
        }

        public static HttpRequest Trace(string url)
        {
            return new HttpRequest(HttpMethod.Trace, url);
        }

        /// <summary>
        /// Use this timeout value unless request specifies its own value for timeout
        /// Throws System.Threading.Tasks.TaskCanceledException when timeout
        /// </summary>
        public static TimeSpan ConnectionTimeout
        {
            get => HttpClientHelper.ConnectionTimeout;
            set => HttpClientHelper.ConnectionTimeout = value;
        }
    }

    public class GetRequest : HttpRequest
    {
        public GetRequest(string url) : base(HttpMethod.Get, url)
        {
        }
    }
    public class PutRequest : HttpRequest
    {
        public PutRequest(string url) : base(HttpMethod.Put, url)
        {
        }
    }
    public class PostRequest : HttpRequest
    {
        public PostRequest(string url) : base(HttpMethod.Post, url)
        {
        }
    }
    public class DeleteRequest : HttpRequest
    {
        public DeleteRequest(string url) : base(HttpMethod.Delete, url)
        {
        }
    }
    public class HeadRequest : HttpRequest
    {
        public HeadRequest(string url) : base(HttpMethod.Head, url)
        {
        }
    }
    public class OptionsRequest : HttpRequest
    {
        public OptionsRequest(string url) : base(HttpMethod.Options, url)
        {
        }
    }
    public class TraceRequest : HttpRequest
    {
        public TraceRequest(string url) : base(HttpMethod.Trace, url)
        {
        }
    }
    public class PatchRequest : HttpRequest
    {
        public PatchRequest(string url) : base(Unirest.PatchMethod, url)
        {
        }
    }
    
    public class GetRequestUrl : HttpRequestUrl
    {
        public GetRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Get, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class PutRequestUrl : HttpRequestUrl
    {
        public PutRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Put, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class PostRequestUrl : HttpRequestUrl
    {
        public PostRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Post, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class DeleteRequestUrl : HttpRequestUrl
    {
        public DeleteRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Delete, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class HeadRequestUrl : HttpRequestUrl
    {
        public HeadRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Head, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class OptionsRequestUrl : HttpRequestUrl
    {
        public OptionsRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Options, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class TraceRequestUrl : HttpRequestUrl
    {
        public TraceRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(HttpMethod.Trace, url, encodeSpaceAsPlusSign)
        {
        }
    }
    public class PatchRequestUrl : HttpRequestUrl
    {
        public PatchRequestUrl(string url, bool encodeSpaceAsPlusSign = true) : base(Unirest.PatchMethod, url, encodeSpaceAsPlusSign)
        {
        }
    }

}
