using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BotBuilderSamples.Models;
using Newtonsoft.Json;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class OpenAIService
    {
        private HttpClient _httpClient;
        public async Task<SentenceResponse> GetResponse(string prompt)
        {
            var json = JsonConvert.SerializeObject(new
            {
                prompt = prompt,
                temperature = 0.5,
                max_tokens = 4000,
                //model = "code-davinci-002"
                model = "text-davinci-003"
            });
            
            _httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + "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");
            //var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci/completions", content);
            var response = await _httpClient.PostAsync(" https://api.openai.com/v1/completions", content);
            //var response = await _httpClient.PostAsync("https://api.openai.com/v1/embeddings", content);
            if (!response.IsSuccessStatusCode)
            {
                    // Handle error
            }
            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SentenceResponse>(responseJson);
            return result;
        }
    }
}