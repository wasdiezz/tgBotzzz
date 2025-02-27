﻿using tgBot.org.example.ApiWorker;
using tgBot.org.example.Buttons;
using tgBot.org.example.statemachine;

namespace tgBot.org.example.service.logic;

public class HistoryLogic
{
    private History _history;

    public HistoryLogic()
    {
        _history = new History();
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
            List<History> historyApplications =
                (List<History>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic++;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            History currentHistories = historyApplications[currentHistoriesLogic - 1];

            textFromUser =
                $"userId:{currentHistories.UserId}\nid:{currentHistories.Id}\ntitle:{currentHistories.Title}\nbody:{currentHistories.Title}";

            //   textFromUser = $"Номер заявки: {currentHistories.IdHistoryApplication} \nСтатус: {currentHistories.Status} \nАдерс: {currentHistories.Address} \nКабинет: {currentHistories.Cabinet} \nФИО: {currentHistories.Fullname} \nТелефон: {currentHistories.NumberPhone} \nДата создания: {currentHistories.DateTime} \nПроблема: {currentHistories.Description}");

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

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

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
            List<History> historyApplications =
                (List<History>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic++;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            History currentHistories = historyApplications[currentHistoriesLogic - 1];

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
            List<History> historyApplications =
                (List<History>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic--;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            History currentHistories = historyApplications[currentHistoriesLogic - 1];

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

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

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
            List<History> historyApplications =
                (List<History>)transmittedData.DataStorage.Get("historyApplications");

            int countHistoriesLogic = (int)transmittedData.DataStorage.Get("countHistoriesLogic");
            int currentHistoriesLogic = (int)transmittedData.DataStorage.Get("currentHistoriesLogic");

            currentHistoriesLogic--;

            transmittedData.DataStorage.Add("currentHistoriesLogic", currentHistoriesLogic);

            History currentHistories = historyApplications[currentHistoriesLogic - 1];

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

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
    }
}