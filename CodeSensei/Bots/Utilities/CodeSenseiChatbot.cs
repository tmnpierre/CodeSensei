using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Interfaces;
using CodeSensei.Services;
using CodeSensei.Data.Repositories.Interfaces;
using CodeSensei.Data.Models;
using System.Web;

namespace CodeSensei.Bots.Utilities
{
    public class CodeSenseiChatbot : ActivityHandler
    {
        private readonly IChatbotHandler _visualStudioShortcutsHandler;
        private readonly ILogger<CodeSenseiChatbot> _logger;
        private readonly FeedbackManager _feedbackManager;
        private readonly IRepository<FeedbackRecord> _feedbackRepository;

        public CodeSenseiChatbot(IChatbotHandler visualStudioShortcutsHandler,
                                 ILogger<CodeSenseiChatbot> logger,
                                 FeedbackManager feedbackManager,
                                 IRepository<FeedbackRecord> feedbackRepository)
        {
            _visualStudioShortcutsHandler = visualStudioShortcutsHandler;
            _logger = logger;
            _feedbackManager = feedbackManager;
            _feedbackRepository = feedbackRepository;
        }

        private readonly HttpClient _httpClient;

        public CodeSenseiChatbot(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var userMessage = turnContext.Activity.Text;
            var witResponse = await GetIntentFromWitAi(userMessage);
        }

        private async Task<string> GetIntentFromWitAi(string message)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["q"] = message;
            queryString["v"] = "2021-05-13";

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://api.wit.ai/message?{queryString}"),
                Headers = {
                    { "Authorization", "Bearer TOKEN_WIT.AI" },
                },
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return body;
        }


        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded,
                                                          ITurnContext<IConversationUpdateActivity> turnContext,
                                                          CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Welcome to the CodeSenseiChatbot, {member.Name}!"), cancellationToken);
                }
            }
        }
    }
}
