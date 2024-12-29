using System.Text;
using ConsoleAppTetsBot.org.example.ApiWorker;
using ConsoleAppTetsBot.org.example.Buttons;
using ConsoleAppTetsBot.org.example.statemachine;

namespace ConsoleAppTetsBot.org.example.service.logic;

public class HistoryLogic
{
    private HistoryApplication _historyApplication;

    public HistoryLogic()
    {
        _historyApplication = new HistoryApplication();
    }

    public BotTextMessage ProcessWaitingShowHistory(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.Next.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";
            return new BotTextMessage(
                textFromUser
            );
        }

        if (textFromUser.Equals(InlineButtonsStorage.Next.CallBackData))
        {
            List<HistoryApplication> historyApplications =
                (List<HistoryApplication>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic++;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            HistoryApplication currentHistories = historyApplications[currentHistoriesLogic - 1];

            textFromUser =
                $"userId:{currentHistories.UserId}\nid:{currentHistories.Id}\ntitle:{currentHistories.Title}\nbody:{currentHistories.Title}";

            //   textFromUser = $"Заявка номер: {currentHistories.IdHistoryApplication} \nСтатус: {currentHistories.Status} \nАдерс: {currentHistories.Address} \nКабинет: {currentHistories.Cabinet} \nФИО: {currentHistories.Fullname} \nТелефон: {currentHistories.NumberPhone} \nДата создания: {currentHistories.DateTime} \nПроблема: {currentHistories.Description}");

            if (currentHistoriesLogic == countHistoriesLogic)
            {
                transmittedData.State = State.WaitingLastShowCommands;
                return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
            }

            transmittedData.State = State.WaitingMiddleShowCommands;

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetMiddleShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Выберите то что вы хотите.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
    }

    public BotTextMessage processWaitingMiddleShowCommands(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.Next.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Next.CallBackData))
        {
            List<HistoryApplication> historyApplications =
                (List<HistoryApplication>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic++;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            HistoryApplication currentHistories = historyApplications[currentHistoriesLogic - 1];

            textFromUser =
                $"userId:{currentHistories.UserId}\nid:{currentHistories.Id}\ntitle:{currentHistories.Title}\nbody:{currentHistories.Title}";

            //     $"Заявка номер: {currentHistories.IdHistoryApplication} \nСтатус: {currentHistories.Status} \nАдерс: {currentHistories.Address} \nКабинет: {currentHistories.Cabinet} \nФИО: {currentHistories.Fullname} \nТелефон: {currentHistories.NumberPhone} \nДата создания: {currentHistories.DateTime} \nПроблема: {currentHistories.Description}");

            if (currentHistoriesLogic == countHistoriesLogic)
            {
                transmittedData.State = State.WaitingLastShowCommands;

                return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
            }

            transmittedData.State = State.WaitingMiddleShowCommands;

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetMiddleShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            List<HistoryApplication> historyApplications =
                (List<HistoryApplication>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic--;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            HistoryApplication currentHistories = historyApplications[currentHistoriesLogic - 1];

            textFromUser =
                $"userId:{currentHistories.UserId}\nid:{currentHistories.Id}\ntitle:{currentHistories.Title}\nbody:{currentHistories.Body}";

            //     $"Заявка номер: {currentHistories.IdHistoryApplication} \nСтатус: {currentHistories.Status} \nАдерс: {currentHistories.Address} \nКабинет: {currentHistories.Cabinet} \nФИО: {currentHistories.Fullname} \nТелефон: {currentHistories.NumberPhone} \nДата создания: {currentHistories.DateTime} \nПроблема: {currentHistories.Description}");

            if (currentHistoriesLogic == 1)
            {
                transmittedData.State = State.WaitingFirstShowCommands;

                return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetNextShowKeyboard);
            }

            transmittedData.State = State.WaitingMiddleShowCommands;

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetMiddleShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Выберите то что вы хотите.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
    }

    public BotTextMessage processWaitingLastShowCommands(string textFromUser, TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            List<HistoryApplication> historyApplications =
                (List<HistoryApplication>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic--;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            HistoryApplication currentHistories = historyApplications[currentHistoriesLogic - 1];

            textFromUser =
                $"userId:{currentHistories.UserId}\nid:{currentHistories.Id}\ntitle:{currentHistories.Title}\nbody:{currentHistories.Title}";

            // stringBuilder.AppendLine(
            //     $"Заявка номер: {currentHistories.IdHistoryApplication} \nСтатус: {currentHistories.Status} \nАдерс: {currentHistories.Address} \nКабинет: {currentHistories.Cabinet} \nФИО: {currentHistories.Fullname} \nТелефон: {currentHistories.NumberPhone} \nДата создания: {currentHistories.DateTime} \nПроблема: {currentHistories.Description}");

            if (countHistoriesLogic == 1)
            {
                transmittedData.State = State.WaitingFirstShowCommands;

                return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
            }

            transmittedData.State = State.WaitingMiddleShowCommands;

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetMiddleShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Выберите то что вы хотите.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
    }
}