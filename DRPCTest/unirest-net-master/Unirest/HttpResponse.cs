using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace unirest_net.http
{
    public class HttpResponse<T>
    {
        public int Code { get; }

        public Dictionary<string, string> Headers { get; }

        public T Body { get; set; }

        public Stream Raw { get; }

        internal HttpResponse(int code, Dictionary<string, string> headers, T body, Stream raw)
        {
            Code = code;
            Headers = headers;
            Body = body;
            Raw = raw;
        }

        public HttpResponse(HttpResponseMessage response)
        {
            Headers = new Dictionary<string, string>();
            Code = (int)response.StatusCode;

            if (response.Content != null)
            {
                var streamTask = response.Content.ReadAsStreamAsync();
                Task.WaitAll(streamTask);
                Raw = streamTask.Result;

                if (typeof(T) == typeof(string))
                {
                    var stringTask = response.Content.ReadAsStringAsync();
                    Task.WaitAll(stringTask);
                    Body = (T)(object)stringTask.Result;
                }
                else if (typeof(Stream) == typeof(T))
                {
                    Body = (T)(object)Raw;
                }
                else
                {
                    var stringTask = response.Content.ReadAsStringAsync();
                    Task.WaitAll(stringTask);
                    Body = JsonConvert.DeserializeObject<T>(stringTask.Result);
                }
            }

            foreach (var header in response.Headers)
            {
                Headers.Add(header.Key, header.Value.First());
            }
        }
    }
}
