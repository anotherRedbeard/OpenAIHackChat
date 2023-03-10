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
using Microsoft.Extensions.Configuration;

namespace Microsoft.BotBuilderSamples.Bots
{
    public class EchoBot : ActivityHandler
    {
        private readonly IConfiguration _iConfig;
        private HttpClient _httpClient;

        public EchoBot(IConfiguration iconfig)
        {
            _iConfig = iconfig;
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = $"Echo: {turnContext.Activity.Text}";
            var openAIService = new OpenAIService(_iConfig);
            var result = await openAIService.GetResponse(turnContext.Activity.Text);

            await turnContext.SendActivityAsync(MessageFactory.Text(result.choices[0].text, result.choices[0].text), cancellationToken);
        }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        var welcomeText = "Hello and welcome!";
        await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
        // foreach (var member in membersAdded)
        // {
        //     if (member.Id != turnContext.Activity.Recipient.Id)
        //     {
        //     }
        // }
    }
    }
}
