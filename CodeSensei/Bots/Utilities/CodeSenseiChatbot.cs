using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Interfaces;
using CodeSensei.Services;
using CodeSensei.Data.Repositories.Interfaces;
using CodeSensei.Data.Models;

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

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageText = turnContext.Activity.Text.ToLower();

            if (messageText.Contains("affichertous"))
            {
                var feedbacks = await _feedbackRepository.GetAllAsync();
                string response = "Voici tous les feedbacks:\n";
                foreach (var feedback in feedbacks)
                {
                    response += $"- {feedback.UserFeedback} (from {feedback.UserId} at {feedback.Timestamp})\n";
                }
                await turnContext.SendActivityAsync(MessageFactory.Text(response), cancellationToken);
            }
            else if (messageText.Contains("visual studio"))
            {
                await _visualStudioShortcutsHandler.HandleAsync(turnContext, cancellationToken);
            }
            else
            {
                await turnContext.SendActivityAsync("Je ne suis pas sûr de comprendre. Pouvez-vous reformuler ?", cancellationToken: cancellationToken);
            }

            await _feedbackManager.RequestFeedbackAsync(turnContext, cancellationToken);
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
