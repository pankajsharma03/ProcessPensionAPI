using System;
using System.Net.Http;

namespace ProcessPensionAPI
{
    public class Client
    {
        public HttpClient Authapi()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://52.224.74.57/");
            //client.BaseAddress = new Uri("http://localhost:35485/");

            return client;
        }

        public HttpClient PensionerDetailsAPI()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://20.237.74.118/");
            //client.BaseAddress = new Uri("http://localhost:4374/");

            return client;

        }
        
    }
}
