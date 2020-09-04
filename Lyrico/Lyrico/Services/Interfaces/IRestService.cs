using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lyrico.Services.Interfaces
{
    public interface IRestService
    {
        Task<string> Get(Uri uri, string key, string value);
        Task<string> Get(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType);
        Task<string> Post(Uri uri, string key, string value);
        Task<string> Post(Uri uri, Dictionary<string, string> headers, Dictionary<string, string> bodyParams, string contentType);
    }
}
