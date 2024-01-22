using Microsoft.Bot.Builder;

namespace CodeSensei.Bots.Interfaces
{
    public interface IFeedbackManager
    {
        Task RequestFeedbackAsync(ITurnContext turnContext, CancellationToken cancellationToken);
    }
}
