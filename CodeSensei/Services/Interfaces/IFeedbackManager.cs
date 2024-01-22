using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace CodeSensei.Bots.Interfaces
{
    public interface IFeedbackManager
    {
        Task RequestFeedbackAsync(ITurnContext turnContext, CancellationToken cancellationToken);
        Task HandleFeedbackAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken);
    }
}
