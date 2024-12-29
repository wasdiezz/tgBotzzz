using ConsoleAppTetsBot.org.example.statemachine;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace ConsoleAppTetsBot;

public class Bot
{
    private TelegramBotClient _botClient;
    private CancellationTokenSource _cancellationTokenSource;

    public Bot()
    {
        _botClient = new TelegramBotClient("8108328648:AAEc9MytfhK1aypmrQ__MUVBteFljBbo9zs");
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public void Start()
    {
        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = Array.Empty<UpdateType>()
        };

        BotHandlers botHandlers = new BotHandlers();

        _botClient.StartReceiving(
            botHandlers.HandleUpdateAsync,
            botHandlers.HandlePollingErrorAsync,
            receiverOptions,
            _cancellationTokenSource.Token
        );
    }

    public string GetBotName()
    {
        return _botClient.GetMeAsync().Result.Username;
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }
}