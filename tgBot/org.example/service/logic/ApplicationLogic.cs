using System.Text;
using tgBot.org.example.ApiWorker;
using tgBot.org.example.Buttons;
using tgBot.org.example.statemachine;
using ApplicationId = tgBot.org.example.ApiWorker.ApplicationId;

namespace tgBot.org.example.service.logic;

public class ApplicationLogic
{
    private ApiWorker.ApiWorker _apiWorker;

    public ApplicationLogic()
    {
        _apiWorker = new ApiWorker.ApiWorker();
    }

    #region ввод номера кабинета

    public BotTextMessage ProcessWaitingInputCabinetNumber(string textFromUser,
        TransmittedData transmittedData)
    {
        if (string.IsNullOrEmpty(textFromUser))
        {
            return new BotTextMessage("Ошибка ввода номера кабинета. Введите число.");
        }

        if (textFromUser.Length > 50)
        {
            return new BotTextMessage(
                "Ошибка. Максимальная длина номера кабинета 50 символов. Пожалуйста, введите номер кабинета заново.");
        }

        transmittedData.DataStorage.Add("cabinetNumber", textFromUser);

        transmittedData.State = State.WaitingInputFullName;

        return new BotTextMessage("Номер кабинета успешно сохранен\nТеперь, сообщением отправьте своё ФИО:");
    }

    #endregion

    #region ввод фио

    public BotTextMessage ProcessWaitingInputFullName(string textFromUser,
        TransmittedData transmittedData)
    {
        if (string.IsNullOrEmpty(textFromUser))
        {
            return new BotTextMessage("Пожалуйста, введите ФИО.");
        }

        if (textFromUser.Length > 150)
        {
            return new BotTextMessage("Ошибка. Максимальная длина ФИО 100 символов. Пожалуйста, введите ФИО заново.");
        }

        transmittedData.DataStorage.Add("fullName", textFromUser);

        transmittedData.State = State.WaitingInputNumberPhone;

        return new BotTextMessage("Ваше ФИО успешно сохранено\nТеперь, введите свой номер телефона:");
    }

    #endregion

    #region ввод номера телефона

    public BotTextMessage ProcessWaitingInputNumberPhone(string textFromUser,
        TransmittedData transmittedData)
    {
        if (string.IsNullOrEmpty(textFromUser))
        {
            return new BotTextMessage("Пожалуйста, введите номер телефона.");
        }

        if (textFromUser.Length > 50)
        {
            return new BotTextMessage(
                "Ошибка. Максимальная длина номера телефона 50 символов. Пожалуйста, введите номер телефона заново.");
        }

        transmittedData.DataStorage.Add("numberPhone", textFromUser);

        transmittedData.State = State.WaitingDescriptionProblem;

        return new BotTextMessage("Номер телефона успешно сохранен\nТеперь, опишите проблему:");
    }

    #endregion

    #region ввод описание проблемы

    public BotTextMessage ProcessWaitingDescriptionProblem(string textFromUser,
        TransmittedData transmittedData)
    {
        if (string.IsNullOrEmpty(textFromUser))
        {
            return new BotTextMessage("Ошибка. Не может быть пустое сообщение.");
        }

        if (textFromUser.Length > 100)
        {
            return new BotTextMessage(
                "Ошибка. Максимальная длина для опсиания проблемы 100 символов. Пожалуйста, введите описание проблемы заново.");
        }

        transmittedData.DataStorage.Add("descriptionProblem", textFromUser);

        transmittedData.State = State.WaitingQuestionAddPhoto;

        textFromUser = "Описание проблемы успешно записано. Хотите добавить фото?";

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetQuestionPhotoKeyboard);
    }

    #endregion

    #region обработчик нажатия кнопок "хотите ли вы добавить фото?", если не добавили фото, то просто выводим данные заявки для верификации.

    public BotTextMessage ProcessWaitingQuestionAddPhoto(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.YesSendPhoto.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.NoSendPhoto.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.YesSendPhoto.CallBackData))
        {
            textFromUser = "Отправьте фото, чтобы прикрепить его к заявке";
            
            if (string.IsNullOrEmpty(textFromUser))
            {
                return new BotTextMessage("Сообщение не может быть пустое, отправьте фотографию");
            }

            transmittedData.State = State.WaitingPhoto;
            
            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.NoSendPhoto.CallBackData))
        {
            StringBuilder stringBuilder = new StringBuilder("Пожалуйста проверьте правильность данных:\n\n");

            stringBuilder.Append("Адрес: ").Append(transmittedData.DataStorage.Get("addressPlace"))
                .Append("\n");

            stringBuilder.Append("Номер кабинета: ").Append(transmittedData.DataStorage.Get("cabinetNumber"))
                .Append("\n");

            stringBuilder.Append("ФИО: ").Append(transmittedData.DataStorage.Get("fullName")).Append("\n");

            stringBuilder.Append("Номер телефона: ").Append(transmittedData.DataStorage.Get("numberPhone"))
                .Append("\n");

            stringBuilder.Append("Описание проблемы: ").Append(transmittedData.DataStorage.Get("descriptionProblem"))
                .Append("\n");

            textFromUser = stringBuilder.ToString();

            transmittedData.State = State.WaitingDataVerificationWithoutPhoto;

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetVerificationDataKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion

    #region обработка фото + верификация данных заявки

    public BotTextMessage ProcessWaitingPhoto(string textFromUser, TransmittedData transmittedData)
    {
        StringBuilder stringBuilder =
            new StringBuilder("Фото успешно прикреплено. Пожалуйста проверьте правильность данных:\n\n");

        stringBuilder.Append("Адрес: ").Append(transmittedData.DataStorage.Get("addressPlace"))
            .Append("\n");

        stringBuilder.Append("Номер кабинета: ").Append(transmittedData.DataStorage.Get("cabinetNumber"))
            .Append("\n");

        stringBuilder.Append("ФИО: ").Append(transmittedData.DataStorage.Get("fullName")).Append("\n");

        stringBuilder.Append("Номер телефона: ").Append(transmittedData.DataStorage.Get("numberPhone"))
            .Append("\n");

        stringBuilder.Append("Описание проблемы: ").Append(transmittedData.DataStorage.Get("descriptionProblem"))
            .Append("\n");

        textFromUser = stringBuilder.ToString();

        transmittedData.State = State.WaitingDataVerification;

        return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetVerificationDataKeyboard);
    }

    #endregion

    #region отправка в апи с фоткой заявки

    public BotTextMessage ProcessWaitingDataVerification(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.SendApplication.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.CancelApplication.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SendApplication.CallBackData))
        {
            transmittedData.State = State.WaitingReadApplication;

            Application application = new Application()
            {
                UserId = (long)transmittedData.DataStorage.Get("userId"),
                AddressId = (int)transmittedData.DataStorage.Get("addressId"),
                NumberCabinet = (string)transmittedData.DataStorage.Get("cabinetNumber"),
                FullName = (string)transmittedData.DataStorage.Get("fullName"),
                NumberPhone = (string)transmittedData.DataStorage.Get("numberPhone"),
                DescriptionProblem = (string)transmittedData.DataStorage.Get("descriptionProblem"),
                Photo = (string)transmittedData.DataStorage.Get("imageUrl")
            };

            _apiWorker.AddNewApplication(application);


            // textFromUser =
            //     $"UserId: {application.UserId} \nAddressId: {application.AddressId}, \nnumber cabinet: {application.NumberCabinet}, \nfullname: {application.FullName}, \nnumber phone: {application.NumberPhone}, \ndescription problem: {application.DescriptionProblem} \nurl photo: {application.Photo}";

            ApplicationId applicationId = _apiWorker.GetByIdApplication();
            
            textFromUser =
                $"Заявка {applicationId.Id} успешно создана! Вам придет уведомление, когда статус заявки будет изменен.";

            transmittedData.DataStorage.Clear();

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackToMenuKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.CancelApplication.CallBackData))
        {
            transmittedData.State = State.WaitingApplication;

            textFromUser = "Пожалуйста, выберите адрес:";

            transmittedData.DataStorage.Clear();

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetAddressKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion

    #region отправка в апи без фотки заявки

    public BotTextMessage ProcessWaitingDataVerificationWithoutPhoto(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.SendApplication.CallBackData) &&
            !textFromUser.Equals(InlineButtonsStorage.CancelApplication.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
        }

        if (textFromUser.Equals(InlineButtonsStorage.SendApplication.CallBackData))
        {
            transmittedData.State = State.WaitingReadApplication;

            Application application = new Application()
            {
                UserId = (long)transmittedData.DataStorage.Get("userId"),
                AddressId = (int)transmittedData.DataStorage.Get("addressId"),
                NumberCabinet = (string)transmittedData.DataStorage.Get("cabinetNumber"),
                FullName = (string)transmittedData.DataStorage.Get("fullName"),
                NumberPhone = (string)transmittedData.DataStorage.Get("numberPhone"),
                DescriptionProblem = (string)transmittedData.DataStorage.Get("descriptionProblem"),
                Photo = ""
            };

            _apiWorker.AddNewApplication(application);

            // textFromUser =
            //     $"UserId: {application.UserId} \nAddressId: {application.AddressId}, \nnumber cabinet: {application.NumberCabinet}, \nfullname: {application.FullName}, \nnumber phone: {application.NumberPhone}, \ndescription problem: {application.DescriptionProblem} \nurl photo: {application.Photo}";

            ApplicationId applicationId = _apiWorker.GetByIdApplication();

            textFromUser =
                $"Заявка {applicationId.Id} успешно создана! Вам придет уведомление, когда статус заявки будет изменен.";

            transmittedData.DataStorage.Clear();

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetBackToMenuKeyboard);
        }

        if (textFromUser.Equals(InlineButtonsStorage.CancelApplication.CallBackData))
        {
            transmittedData.State = State.WaitingApplication;

            textFromUser = "Пожалуйста, выберите адрес:";

            transmittedData.DataStorage.Clear();

            return new BotTextMessage(textFromUser, InlineKeyboardsStorage.GetAddressKeyboard);
        }

        return new BotTextMessage(textFromUser);
    }

    #endregion

    #region обработка нажатия кнопки главное меню после успешной подачи заявки

    public BotTextMessage ProcessWaitingReadApplication(string textFromUser,
        TransmittedData transmittedData)
    {
        if (!textFromUser.Equals(InlineButtonsStorage.BackToMenu.CallBackData))
        {
            textFromUser = "Ошибка. Нажмите на кнопку.";

            return new BotTextMessage(textFromUser);
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