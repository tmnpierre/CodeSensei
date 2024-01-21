using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Handlers;
using CodeSensei.Bots.Interfaces;

namespace CodeSensei.Bots
{
    public class CodeSenseiChatbot : ActivityHandler
    {
        private readonly IChatbotHandler _visualStudioShortcutsHandler;

        public CodeSenseiChatbot(IChatbotHandler visualStudioShortcutsHandler)
        {
            _visualStudioShortcutsHandler = visualStudioShortcutsHandler;
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageText = turnContext.Activity.Text.ToLower();

            if (messageText.Contains("visual studio"))
            {
                await _visualStudioShortcutsHandler.HandleAsync(turnContext, cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync("Je ne suis pas sûr de comprendre. Pouvez-vous reformuler ?");
            }
        }
    }
}
