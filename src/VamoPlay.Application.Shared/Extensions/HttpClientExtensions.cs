﻿using VamoPlay.Application.Shared.Exceptions;
using VamoPlay.Application.Shared.ViewModels.Response;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VamoPlay.Application.Shared.Extensions
{
    public static class HttpClientExtensions
    {
        #region public methods implementations

        public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
            => await SendAndDeserialize<T>(HttpMethod.Get, client, requestUri, null);

        public static async Task<T> PostAndDeserialize<T>(this HttpClient client, string requestUri, object data)
            => await SendAndDeserialize<T>(HttpMethod.Post, client, requestUri, data);

        public static async Task<T> PutAndDeserialize<T>(this HttpClient client, string requestUri, object data)
            => await SendAndDeserialize<T>(HttpMethod.Put, client, requestUri, data);

        public static async Task<T> DeleteAndDeserialize<T>(this HttpClient client, string requestUri)
            => await SendAndDeserialize<T>(HttpMethod.Delete, client, requestUri, null);

        #endregion

        #region Private Methods

        private static async Task<T> SendAndDeserialize<T>(HttpMethod method, HttpClient client, string requestUri, object data)
        {
            var response = await client.SendAsync(CreateRequestMessage(method, requestUri, data));
            return await ValidateAndReturnResponseData<T>(response);
        }

        private static HttpRequestMessage CreateRequestMessage(HttpMethod method, string requestUri, object content = null)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUri, UriKind.RelativeOrAbsolute),
                Method = method
            };

            if (content != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            return request;
        }

        private static async Task<T> ValidateAndReturnResponseData<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseResponseViewModel>(resultString);

            if (baseResponse.Errors.Any())
                throw new VamoPlayException(baseResponse.Errors.FirstOrDefault());

            var getResponseDataJson = JsonConvert.SerializeObject(baseResponse?.Data);
            return JsonConvert.DeserializeObject<T>(getResponseDataJson);
        }

        #endregion
    }
}
