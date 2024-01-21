using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace CodeSensei.Bots.Interfaces
{
    public interface IChatbotHandler
    {
        Task HandleAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken);
    }
}
