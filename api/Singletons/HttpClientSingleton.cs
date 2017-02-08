using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace api
{
    public class HttpClientSingleton : HttpClient
    {
        // This approach si thread safe
        // Allows threads to queue up to use the shared resource, prevents thread collision
        private static readonly object mutex = new object();
        private static HttpClientSingleton HttpClient;
        private HttpClientSingleton()
        {
            return;
        }

        public static HttpClientSingleton Instance()
        {
            if (HttpClient == null)
            {
                // Allows threads to queue up to use the shared resource, prevents thread collision
                lock (mutex)
                {
                    if(HttpClient == null)
                    {
                        HttpClient = new HttpClientSingleton();
                        // Sets the keep-alive header to false, we want to do this to ensure DNS entries are honoured
                        // http://byterot.blogspot.co.uk/2016/07/singleton-httpclient-dns.html
                        HttpClient.DefaultRequestHeaders.ConnectionClose = true;
                    }
                }
            }
            return HttpClient;
        }
    }
}
