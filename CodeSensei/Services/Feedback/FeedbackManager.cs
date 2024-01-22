using System.Threading.Tasks;
using CodeSensei.Bots.Interfaces;
using CodeSensei.Data.Contexts;
using CodeSensei.Data.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Threading;

namespace CodeSensei.Services
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly FeedbackContext _context;

        public FeedbackManager(FeedbackContext context)
        {
            _context = context;
        }

        public async Task RequestFeedbackAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var feedbackMessage = "Avez-vous trouvé cette information utile ? Répondez par 'oui' ou 'non'.";
            await turnContext.SendActivityAsync(MessageFactory.Text(feedbackMessage), cancellationToken);
        }

        public async Task HandleFeedbackAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var feedback = turnContext.Activity.Text?.Trim().ToLowerInvariant();

            await SaveFeedbackToDatabase(turnContext.Activity.From.Id, feedback);
            var thankYouMessage = "Merci pour votre retour !";
            await turnContext.SendActivityAsync(MessageFactory.Text(thankYouMessage), cancellationToken);
        }

        private async Task SaveFeedbackToDatabase(string userId, string feedback)
        {
            var feedbackRecord = new FeedbackRecord
            {
                UserId = userId,
                UserFeedback = feedback
            };

            _context.FeedbackRecords.Add(feedbackRecord);
            await _context.SaveChangesAsync();
        }
    }
}
