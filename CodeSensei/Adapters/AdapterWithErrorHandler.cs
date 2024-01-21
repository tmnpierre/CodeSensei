using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CodeSenseiChatbot.Adapters
{
    public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
    {
        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
        {
            OnTurnError = async (context, exception) =>
            {
                logger.LogError(exception, "Exception non gérée détectée");
                await context.SendActivityAsync("Désolé, il semble qu'une erreur soit survenue.");
            };
        }
    }
}
