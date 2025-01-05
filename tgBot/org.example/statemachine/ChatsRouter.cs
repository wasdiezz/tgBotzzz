using Telegram.Bot;
using Telegram.Bot.Types.InputFiles;
using tgBot.org.example.service;

namespace tgBot.org.example.statemachine;

public class ChatsRouter
{
    private Dictionary<long, TransmittedData> _routings;
    private ServiceManager _serviceManager;
    private readonly TelegramBotClient _botClient;

    public ChatsRouter()
    {
        _botClient = new TelegramBotClient("8108328648:AAEc9MytfhK1aypmrQ__MUVBteFljBbo9zs");
        _routings = new Dictionary<long, TransmittedData>();
        _serviceManager = new ServiceManager();
    }

    public async Task<BotTextMessage> Route(long chatId, string textFromUser, InputOnlineFile? photo = null)
    {
        if (!_routings.ContainsKey(chatId))
        {
            _routings[chatId] = new TransmittedData(chatId);
        }

        TransmittedData transmittedData = _routings[chatId];

        transmittedData.DataStorage.Add("userId", chatId);

        if (photo != null)
        {
            try
            {
                var fileInfo = await _botClient.GetFileAsync(photo.FileId);

                if (fileInfo != null)
                {
                    string fileUrl =
                        $"https://api.telegram.org/file/bot{_botClient}/{fileInfo.FilePath}";

                    transmittedData.DataStorage.Add("imageUrl", fileUrl);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при обработке фото", ex);
            }
        }

        return _serviceManager.ProcessBotUpdate(textFromUser, transmittedData);
    }
}