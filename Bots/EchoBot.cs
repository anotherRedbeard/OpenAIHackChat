// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Microsoft.BotBuilderSamples.Bots;
using Microsoft.BotBuilderSamples.Models;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        private HttpClient _httpClient;

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
            var json = JsonConvert.SerializeObject(new
            {
                prompt = turnContext.Activity.Text,
                temperature = 0.5,
                max_tokens = 50,
                model = "text-davinci-003"

            });
            _httpClient = new HttpClient();
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + "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");
            //var response = await _httpClient.PostAsync("https://api.openai.com/v1/engines/davinci/completions", content);
            var response = await _httpClient.PostAsync(" https://api.openai.com/v1/completions", content);


            var responseJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SentenceResponse>(responseJson);
            await turnContext.SendActivityAsync(MessageFactory.Text(result.choices[0].text, result.choices[0].text), cancellationToken);
        }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        var welcomeText = "Hello and welcome!";
        foreach (var member in membersAdded)
        {
            if (member.Id != turnContext.Activity.Recipient.Id)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
            }
        }
    }
    }
}
