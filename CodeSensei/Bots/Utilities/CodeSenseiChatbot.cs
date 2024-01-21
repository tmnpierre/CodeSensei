using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Handlers;
using CodeSensei.Bots.Interfaces;
using Microsoft.Extensions.Logging;

namespace CodeSensei.Bots.Utilities
{
    public class CodeSenseiChatbot : ActivityHandler
    {
        private readonly IChatbotHandler _visualStudioShortcutsHandler;
        private readonly ILogger<CodeSenseiChatbot> _logger;

        public CodeSenseiChatbot(IChatbotHandler visualStudioShortcutsHandler, ILogger<CodeSenseiChatbot> logger)
        {
            _visualStudioShortcutsHandler = visualStudioShortcutsHandler;
            _logger = logger;
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
        }
    }
}
