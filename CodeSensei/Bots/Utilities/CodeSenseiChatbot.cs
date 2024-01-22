using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Handlers;
using CodeSensei.Bots.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using CodeSensei.Services;

namespace CodeSensei.Bots.Utilities
{
    public class CodeSenseiChatbot : ActivityHandler
    {
        private readonly IChatbotHandler _visualStudioShortcutsHandler;
        private readonly ILogger<CodeSenseiChatbot> _logger;
        private readonly FeedbackManager _feedbackManager;

        public CodeSenseiChatbot(
            IChatbotHandler visualStudioShortcutsHandler,
            ILogger<CodeSenseiChatbot> logger,
            FeedbackManager feedbackManager)
        {
            _visualStudioShortcutsHandler = visualStudioShortcutsHandler;
            _logger = logger;
            _feedbackManager = feedbackManager;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageText = turnContext.Activity.Text.ToLower();
            _logger.LogInformation("Message reçu: {Message}", messageText);

            if (messageText.Contains("visual studio"))
            {
                _logger.LogInformation("Délégation à VisualStudioShortcutsHandler");
                await _visualStudioShortcutsHandler.HandleAsync(turnContext, cancellationToken);
            }
            else
            {
                _logger.LogWarning("Message non reconnu: {Message}", messageText);
                await turnContext.SendActivityAsync("Je ne suis pas sûr de comprendre. Pouvez-vous reformuler ?");
            }

            await _feedbackManager.RequestFeedbackAsync(turnContext, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
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
