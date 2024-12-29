using Telegram.Bot.Types.ReplyMarkups;

namespace tgBot;

public class BotTextMessage
{
    public string Text { get; }
    public InlineKeyboardMarkup InlineKeyboardMarkup { get; }

    public BotTextMessage(string text)
    {
        Text = text;
        InlineKeyboardMarkup = null;
    }

    public BotTextMessage(string text, InlineKeyboardMarkup inlineKeyboardMarkup)
    {
        Text = text;
        InlineKeyboardMarkup = inlineKeyboardMarkup;
    }

    public BotTextMessage(InlineKeyboardMarkup inlineKeyboardMarkup)
    {
        Text = null;
        InlineKeyboardMarkup = inlineKeyboardMarkup;
    }
}