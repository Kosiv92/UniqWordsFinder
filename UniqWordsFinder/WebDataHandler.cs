using FileSupport.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebInterractionLib;

namespace UniqWordsFinder
{
    public class WebDataHandler : IDataHandler
    {
        private readonly WebServiceClient _webServiceClient;

        private readonly string[] _data;

        private Dictionary<string, int> _uniqueWords;
                
        public WebDataHandler(WebServiceClient webServiceClient, string[] data)
        {
            _webServiceClient = webServiceClient;
            _data = data;
            _uniqueWords = new Dictionary<string, int>();            
        }

        public int UniqueWordsFound => _uniqueWords.Count;

        public Dictionary<string, int> HandleData()
        {
            var request = CreateRequest(_data);

            HttpContent httpContent = new StringContent(request, UnicodeEncoding.UTF8, "application/json");

            using var response = _webServiceClient.HttpClient.PostAsync(_webServiceClient.Url, httpContent).Result;

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
