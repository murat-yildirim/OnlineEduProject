﻿using OnlineEdu.WebUI.Services.TokenServices;
using System.Net.Http.Headers;

namespace OnlineEdu.WebUI.Helpers
{
    public static class HttpClientInstance
    {
        

        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:7268/api/");


            return client;


        }


    }

}
