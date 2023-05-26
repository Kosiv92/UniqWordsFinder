using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebInterractionLib;

namespace UniqWordsFinder
{
    public class WebServiceClient
    {
        private readonly string _webServiceUrl;

        HttpClient _httpClient;

        public WebServiceClient(string webServiceUrl)
        {
            _webServiceUrl = webServiceUrl;
            _httpClient = new HttpClient();
        }

        public HttpClient HttpClient { get => _httpClient; }

        public string Url { get => _webServiceUrl; }

        public async Task<bool> IsServerAvailable() //проверка доступности сервера
        {
            var testConnectionResult = await new HttpClient().GetAsync(_webServiceUrl);

            if (testConnectionResult.StatusCode == System.Net.HttpStatusCode.OK) return true;
            else return false;
        }

        public async Task<Dictionary<string, int>> PostDataToWebService(string[] data)
        {
            var request = CreateRequest(data);
            
            HttpContent httpContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(_webServiceUrl, httpContent);

            return JsonSerializer.Deserialize<WordsDto>(response.Content.ReadAsStringAsync().Result).WordsCount;                        
        }

        private string CreateRequest(string[] data)
        {
            var request = new StringsDto();
            request.Strings = data;

            return JsonSerializer.Serialize(request);
        }
    }
}
