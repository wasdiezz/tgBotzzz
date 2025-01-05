using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using tgBot.org.example.statemachine;

namespace tgBot;

public class BotHandlers
{
    private ChatsRouter _chatsRouter;

    public BotHandlers()
    {
        _chatsRouter = new ChatsRouter();
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        long chatId = 0;
        int messageId = 0;
        string textFromUser = "";
        InputOnlineFile? photo = null;
        bool canRoute = false;

        switch (update.Type)
        {
            case UpdateType.Message:
                if (update.Message != null)
                {
                    canRoute = true;
                    chatId = update.Message.Chat.Id;
                    messageId = update.Message.MessageId;
                    textFromUser = update.Message.Text;
                }

                if (update.Message.Type == MessageType.Photo)
                {
                    photo = new InputOnlineFile(update.Message.Photo[^1].FileId);
                    textFromUser = "";
                }

                break;

            case UpdateType.CallbackQuery:
                if (update.CallbackQuery != null)
                {
                    canRoute = true;
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    messageId = update.CallbackQuery.Message.MessageId;
                    textFromUser = update.CallbackQuery.Data;
                }

                break;
        }

        if (canRoute)
        {
            try
            {
                BotTextMessage botTextMessage =
                    await Task.Run(() => _chatsRouter.Route(chatId, textFromUser, photo),
                        cancellationToken);

                await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: botTextMessage.Text,
                    replyMarkup: botTextMessage.InlineKeyboardMarkup,
                    cancellationToken: cancellationToken);
            }
            catch (Exception e)
            {
                await botClient.DeleteMessageAsync(
                    chatId: chatId,
                    messageId: messageId,
                    cancellationToken: cancellationToken);
            }
        }
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        string errorMessage = "empty";
        switch (exception)
        {
            case ApiRequestException:
            {
                var ex = exception as ApiRequestException;
                errorMessage = $"Telegram API Error:\n[{ex.ErrorCode}]\n{ex.Message}";
            }
                break;
            default:
            {
                errorMessage = exception.ToString();
            }
                break;
        }

        return Task.CompletedTask;
    }
}