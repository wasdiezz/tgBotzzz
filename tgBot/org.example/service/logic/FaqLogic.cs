using tgBot.org.example.Buttons;
using tgBot.org.example.statemachine;

namespace tgBot.org.example.service.logic;

public class FaqLogic
{
    #region проблемы компьютера

    public BotTextMessage ProcessWaitingViewProblemComputer(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData))
        {
            transmittedData.State = State.WaitingFirstInfoProblemComputer;

            textFromUser = "Отсутствует подключение к сети Интернет\n" +
                           "При отсутствии подключения к сети Интернет на компьютере:\n" +
                           "1. Проверьте кабель сети в компьютере и розетке для кабеля сети;\n" +
                           "2. Если Вы подключены через кабель сети и проверив его концы не обнаружили проблему, то оставьте запрос техническому специалисту.\n" +
                           "\n" +
                           "При отсутствии подключения к сети Интернет на ноутбуке:\n" +
                           "1. Нажмите на значок настроек (шестерня);\n" +
                           "2. Перейдите в раздел сети (WI-FI);\n" +
                           "3. Найдите ближайшую рабочую WI-FI сеть;\n" +
                           "4. Нажмите на кнопку \"Подключиться\".\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData))
        {
            transmittedData.State = State.WaitingSecondInfoProblemComputer;

            textFromUser =
                "1. Проверьте кабель питания (подключен ли он к компьютеру и вставлена ли вилка в розетку);\n" +
                "2. Проверьте кнопку на блоке питания (должна быть в режиме \"I\");\n" +
                "3. Если Ваш компьютер подключен к сети электропитания через удлинитель, проверьте включен ли он;\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData))
        {
            transmittedData.State = State.WaitingThirdInfoProblemComputer;

            textFromUser = "Монитор выводит изображения с помехами;\n" +
                           "\n" +
                           "1. Попробуйте переставить кабель вывода изображения в мониторе\n" +
                           "2. Если монитор мигает, то:\n" +
                           "2.1 Проверьте, включен ли компьютер;\n" +
                           "2.2 Выключите и снова включите монитор.\n" +
                           "\n" +
                           "Монитор не выводит изображение;\n" +
                           "\n" +
                           "1. Проверьте кабель вывода изображения с компьютера на монитор;\n" +
                           "2. Проверьте кабель питания на мониторе;\n" +
                           "3. Проверьте включен ли монитор;\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingQuestions;

            textFromUser = "Выберите, с чем возникла проблема:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingFirstInfoProblemComputer(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemComputer;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Отсутствует подключение к сети Интернет \n2. Не включается компьютер \n3. Проблема с монитором.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingSecondInfoProblemComputer(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemComputer;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Отсутствует подключение к сети Интернет \n2. Не включается компьютер \n3. Проблема с монитором.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingThirdInfoProblemComputer(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemComputer;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Отсутствует подключение к сети Интернет \n2. Не включается компьютер \n3. Проблема с монитором.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion

    #region проблемы принтера

    public BotTextMessage ProcessWaitingViewProblemPrinter(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData))
        {
            transmittedData.State = State.WaitingFirstInfoProblemPrinter;

            textFromUser = "1. Перейдите в настройки Windows;\n" +
                           "2. Зайдите в параметры устройств и выберите категорию с принтерами;\n" +
                           "3. Если в списке подключенных принтеров нет вашего, то запустите поиск;\n" +
                           "3.1. В списке найденных устройств выбери ваш принтер и ожидайте подключения;\n" +
                           "3. Если Ваш принтер подключен к компьютеру, то удалите его и подключите заново.\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData))
        {
            transmittedData.State = State.WaitingSecondInfoProblemPrinter;

            textFromUser =
                "1. При замятии бумаги посмотрите на панель управления принтера и следуйте инструкции;\n" +
                "2. Откройте заднюю панель принтера;\n" +
                "3. Вытащите замятый лист и закройте панель;\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingQuestions;

            textFromUser = "Выберите, с чем возникла проблема:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingFirstInfoProblemPrinter(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemPrinter;

            textFromUser = "Вот список часто возникающих проблем: \n1. Не подключается к компьютеру \n2. Замятие бумаги";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFoursButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingSecondInfoProblemPrinter(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemPrinter;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Не подключается к компьютеру \n2. Замятие бумаги";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFoursButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion

    #region проблемы компьютера

    public BotTextMessage ProcessWaitingViewProblemProjector(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData))
        {
            transmittedData.State = State.WaitingFirstInfoProblemProjector;

            textFromUser = "1. Проверьте, включен ли проектор/интерактивная доска;\n" +
                           "2. Проверьте кабель вывода изображения;\n" +
                           "3. На компьютере нажмите комбинацию клавиш WIN + P и в открывшейся панели выберите \"Повторяющийся\".\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData))
        {
            transmittedData.State = State.WaitingSecondInfoProblemProjector;

            textFromUser = "1. Проверьте кабель питания, вставлен ли он в розетку;\n" +
                           "2. Проверьте рубильник в щитке:\n" +
                           "2.1 Если включен, но при этом нет электричества, обратитесь к заведующему хозяйству по зданию.\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData))
        {
            transmittedData.State = State.WaitingThirdInfoProblemProjector;

            textFromUser = "1. Попробуйте выключить свет в кабинете;\n" +
                           "2. Возьмите пульт и перейдите в настройки проектора, затем перейдите во вкладку \"Изображение\" и измените уровень яркости.\n";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingQuestions;

            textFromUser = "Выберите, с чем возникла проблема:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemSystemShowKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingFirstInfoProblemProjector(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemProjector;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Не выводится изображение \n2. Проектор не включается \n3. Слишком тусклое изображение.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingSecondInfoProblemProjector(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemProjector;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Не выводится изображение \n2. Проектор не включается \n3. Слишком тусклое изображение.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    public BotTextMessage ProcessWaitingThirdInfoProblemProjector(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.FirstInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.SecondInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.ThirdInfo.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.Back.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.Back.CallBackData))
        {
            transmittedData.State = State.WaitingViewProblemProjector;

            textFromUser =
                "Вот список часто возникающих проблем: \n1. Не выводится изображение \n2. Проектор не включается \n3. Слишком тусклое изображение.";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetProblemFiveButtonsKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            transmittedData.State = State.WaitingQuestionsOrApplicationOrHistory;

            textFromUser = "Здравствуйте! Это техническая поддержка МГОК.\nВыберите действие:";

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetStartKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion
}