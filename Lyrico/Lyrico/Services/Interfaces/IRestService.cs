using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Lyrico.Services.Interfaces
{
    public interface IRestService
    {
        Task<HttpResponseMessage> Get(Uri uri, string key, string value);
        Task<HttpResponseMessage> Get(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType);
        Task<HttpResponseMessage> Post(Uri uri, string key, string value);
        Task<HttpResponseMessage> Post(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType);
    }
}
