﻿using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using CodeSensei.Bots.Interfaces;
using CodeSensei.Bots.Enums;
using Microsoft.Extensions.Logging;

namespace CodeSensei.Bots.Handlers
{
    public class VisualStudioShortcutsHandler : IChatbotHandler
    {
        private readonly ILogger<VisualStudioShortcutsHandler> _logger;

        public VisualStudioShortcutsHandler(ILogger<VisualStudioShortcutsHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var messageText = turnContext.Activity.Text.ToLower();
            _logger.LogInformation("Traitement d'un message dans VisualStudioShortcutsHandler: {Message}", messageText);

            switch (GetMessageType(messageText))
            {
                case MessageType.Categories:
                    await ShowCategories(turnContext, cancellationToken);
                    break;
                case MessageType.CodeEditing:
                    await SendCodeEditingShortcuts(turnContext, cancellationToken);
                    break;
                case MessageType.SolutionNavigation:
                    await SendSolutionNavigationShortcuts(turnContext, cancellationToken);
                    break;
                case MessageType.Debugging:
                    await SendDebuggingShortcuts(turnContext, cancellationToken);
                    break;
                case MessageType.OtherShortcuts:
                    await SendOtherUsefulShortcuts(turnContext, cancellationToken);
                    break;
                default:
                    _logger.LogWarning("Message non reconnu ou inattendu: {Message}", messageText);
                    await turnContext.SendActivityAsync("Désolé, je ne comprends pas cette demande. Pouvez-vous reformuler votre question ?");
                    break;
            }
        }

        private MessageType GetMessageType(string messageText)
        {
            if (messageText.Contains("raccourcis") && messageText.Contains("visual studio"))
                return MessageType.Categories;
            if (messageText.Contains("1") || messageText.Contains("navigation et édition du code"))
                return MessageType.CodeEditing;
            if (messageText.Contains("2") || messageText.Contains("navigation dans la solution"))
                return MessageType.SolutionNavigation;
            if (messageText.Contains("3") || messageText.Contains("débogage"))
                return MessageType.Debugging;
            if (messageText.Contains("4") || messageText.Contains("autres raccourcis utiles"))
                return MessageType.OtherShortcuts;

            return MessageType.Unknown;
        }

        private async Task ShowCategories(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string categoriesMessage = "Vous pouvez obtenir des informations sur les catégories de raccourcis clavier en utilisant l'un des mots-clés suivants ou les chiffres correspondants :\n\n" +
                "1. Navigation et Édition du Code\n" +
                "2. Navigation dans la Solution\n" +
                "3. Débogage\n" +
                "4. Autres Raccourcis Utiles\n" +
                "\nPour en savoir plus sur une catégorie particulière, dites simplement le numéro ou le nom de la catégorie.";

            await turnContext.SendActivityAsync(categoriesMessage, cancellationToken: cancellationToken);
        }

        private async Task SendCodeEditingShortcuts(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string shortcutsMessage = "Voici quelques raccourcis clavier utiles pour Navigation et Édition du Code dans Visual Studio :\n\n" +
                "- Ctrl + E, D : Formater le code (indentation, espaces, etc.)\n" +
                "- F12 : Aller à la définition d'une méthode, d'une variable ou d'un type\n" +
                "- Ctrl + - : Retourner à la dernière position du curseur\n" +
                "- Ctrl + Shift + - : Aller à la prochaine position du curseur (après avoir utilisé Ctrl + -)\n" +
                "- Ctrl + ] : Aller à la parenthèse ouvrante ou fermante correspondante\n" +
                "- Ctrl + - (avec un mot sélectionné) : Rechercher toutes les occurrences du mot actuellement sélectionné dans le fichier\n" +
                "- Ctrl + Shift + F : Rechercher dans tous les fichiers du projet\n" +
                "- Ctrl + F : Rechercher dans le fichier actif\n" +
                "- Ctrl + H : Remplacer dans le fichier actif\n" +
                "- Ctrl + / : Commenter ou décommenter une ligne de code\n";

            await turnContext.SendActivityAsync(shortcutsMessage, cancellationToken: cancellationToken);
        }

        private async Task SendSolutionNavigationShortcuts(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string shortcutsMessage = "Voici quelques raccourcis clavier utiles pour Navigation dans la Solution et la Solution Explorer dans Visual Studio :\n\n" +
                "- Ctrl + , : Ouvrir la boîte de dialogue de recherche rapide (Quick Find)\n" +
                "- Ctrl + Alt + L : Ouvrir le Solution Explorer\n" +
                "- Ctrl + Shift + T : Rechercher un fichier dans la solution\n" +
                "- Ctrl + , (dans Solution Explorer) : Rechercher un fichier ou un dossier dans la Solution Explorer\n" +
                "- F4 : Ouvrir les propriétés du projet\n";

            await turnContext.SendActivityAsync(shortcutsMessage, cancellationToken: cancellationToken);
        }

        private async Task SendDebuggingShortcuts(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string shortcutsMessage = "Voici quelques raccourcis clavier utiles pour le Débogage dans Visual Studio :\n\n" +
                "- F5 : Démarrer le débogage\n" +
                "- F9 : Ajouter/Supprimer un point d'arrêt (breakpoint)\n" +
                "- F10 : Passer à l'instruction suivante pendant le débogage (pas d'entrée dans les méthodes)\n" +
                "- F11 : Entrer dans une méthode pendant le débogage\n" +
                "- Shift + F11 : Quitter une méthode pendant le débogage\n" +
                "- Ctrl + Shift + F5 : Redémarrer le débogage sans recompiler\n";

            await turnContext.SendActivityAsync(shortcutsMessage, cancellationToken: cancellationToken);
        }

        private async Task SendOtherUsefulShortcuts(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            string shortcutsMessage = "Voici quelques autres raccourcis clavier utiles dans Visual Studio :\n\n" +
                "- Ctrl + Space : Afficher l'autocomplétion\n" +
                "- Alt + Enter : Afficher les suggestions de corrections (quick fixes)\n" +
                "- Ctrl + K, D : Formater le document entier\n" +
                "- Ctrl + K, C : Commenter la sélection\n" +
                "- Ctrl + K, U : Décommenter la sélection\n" +
                "- Ctrl + K, S : Entourer la sélection avec une structure (if, for, while, etc.)\n" +
                "- Ctrl + K, X : Supprimer la structure englobante\n" +
                "- Ctrl + Tab : Basculer entre les fichiers ouverts\n" +
                "- Ctrl + W : Fermer l'onglet actif\n" +
                "- Ctrl + Shift + W : Fermer tous les onglets sauf l'actif\n" +
                "- Ctrl + Shift + V : Coller du texte en conservant la mise en forme (collage spécial)\n" +
                "- Ctrl + K, V : Coller du texte sans mise en forme (collage simple)\n" +
                "- Ctrl + K, K : Supprimer la ligne courante\n";

            await turnContext.SendActivityAsync(shortcutsMessage, cancellationToken: cancellationToken);
        }
    }
}