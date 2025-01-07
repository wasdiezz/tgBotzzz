using Telegram.Bot;
using Telegram.Bot.Types;
using tgBot.org.example.service;

namespace tgBot.org.example.statemachine;

public class ChatsRouter
{
    private Dictionary<long, TransmittedData> _routings;
    private ServiceManager _serviceManager;

    private TelegramBotClient _botClient;
    private string _token = "8108328648:AAEc9MytfhK1aypmrQ__MUVBteFljBbo9zs";

    public ChatsRouter()
    {
        _botClient = new TelegramBotClient("8108328648:AAEc9MytfhK1aypmrQ__MUVBteFljBbo9zs");
        _routings = new Dictionary<long, TransmittedData>();
        _serviceManager = new ServiceManager();
    }

    public async Task<BotTextMessage> Route(long chatId, string textFromUser, PhotoSize[]? photo = null)
    {
        if (!_routings.ContainsKey(chatId))
        {
            _routings[chatId] = new TransmittedData(chatId);
        }

        TransmittedData transmittedData = _routings[chatId];

        transmittedData.DataStorage.Add("userId", chatId);

        if (photo != null && photo.Length > 0)
        {
            try
            {
                var file = await _botClient.GetFileAsync(photo[photo.Length - 1].FileId);

                var fileUrl = $"https://api.telegram.org/file/bot{_token}/{file.FilePath}";

                transmittedData.DataStorage.Add("imageUrl", fileUrl);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обработке фото", ex);
            }
        }

        return _serviceManager.ProcessBotUpdate(textFromUser, transmittedData);
    }
}