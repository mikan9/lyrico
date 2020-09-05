using Lyrico.Services.Interfaces;
using Lyrico.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Lyrico.Services
{
    public class RestService : IRestService
    {
#if DEBUG
        HttpClientHandler httpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true
        };
#else
        HttpClientHandler httpHandler = new HttpClientHandler();
#endif
        public RestService()
        {
        }

        public async Task<HttpResponseMessage> Get(Uri uri, string key, string value)
        {
            return await Request(uri, null, new Dictionary<string, string> { { key, value } }, null, "GET");
        }
        public async Task<HttpResponseMessage> Get(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType)
        {
            return await Request(uri, headers, bodyParams, contentType, "GET");
        }
        public async Task<HttpResponseMessage> Post(Uri uri, string key, string value)
        {
            return await Request(uri, null, new Dictionary<string, string> { { key, value } }, "application/x-www-form-urlencoded", "POST");
        }
        public async Task<HttpResponseMessage> Post(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType)
        {
            return await Request(uri, headers, bodyParams, contentType, "POST");
        }

        public async Task<HttpResponseMessage> Request(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType, string method)
        {
            using (HttpClient client = new HttpClient(httpHandler, false))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(method), uri))
                {
                    if (!headers.IsNullOrEmpty())
                        foreach (KeyValuePair<string, string> header in headers)
                            request.Headers.Add(header.Key, header.Value);


                    if (!bodyParams.IsNullOrEmpty())
                    {
                        List<string> body = new List<string>();

                        foreach (KeyValuePair<string, string> bodyParam in bodyParams)
                            body.Add($"{bodyParam.Key}={bodyParam.Value}");

                        request.Content = new StringContent(string.Join("&", body));
                    }

                    if (!string.IsNullOrWhiteSpace(contentType))
                        if (MediaTypeHeaderValue.TryParse(contentType, out MediaTypeHeaderValue headerValue))
                            request.Content.Headers.ContentType = headerValue;

                    try
                    {
                        return await client.SendAsync(request);

                        //if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        //{
                        //    response.EnsureSuccessStatusCode();
                        //    return await response.Content.ReadAsStringAsync();
                        //}
                        //else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                        //{
                        //    return null;
                        //}
                        //else return response.StatusCode.ToString();
                    }
                    catch(HttpRequestException e)
                    {
                        return null;
                    }
                    
                }
            }
        }
    }
}
