using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

public class FeedbackManager
{
    public async Task RequestFeedbackAsync(ITurnContext turnContext, CancellationToken cancellationToken)
    {
        var feedbackMessage = "Avez-vous trouvé cette information utile ? Répondez par 'oui' ou 'non'.";
        await turnContext.SendActivityAsync(MessageFactory.Text(feedbackMessage), cancellationToken);
    }

    public async Task HandleFeedbackAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        var feedback = turnContext.Activity.Text?.Trim().ToLowerInvariant();

        // BDD
        // Pseudo-code: SaveFeedbackToDatabase(turnContext.Activity.From.Id, feedback);

        var thankYouMessage = "Merci pour votre retour !";
        await turnContext.SendActivityAsync(MessageFactory.Text(thankYouMessage), cancellationToken);
    }

    private Task SaveFeedbackToDatabase(string userId, string feedback)
    {
        return Task.CompletedTask;
    }
}
