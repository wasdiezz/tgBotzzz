using System.Text;
using tgBot.org.example.ApiWorker;
using tgBot.org.example.Buttons;
using tgBot.org.example.statemachine;

namespace tgBot.org.example.service.logic;

public class ApplicationLogic
{
    private ApplicationApiWorker _applicationApiWorker;
    private ApplicationEntity _applicationEntity;
    private ApplicationEntityWithoutPhoto _applicationEntityWithoutPhoto;

    public ApplicationLogic()
    {
        _applicationApiWorker = new ApplicationApiWorker();
    }

    #region ввод номера кабинета

    public BotTextMessage ProcessWaitingInputCabinetNumber(string textFromUser,
        TransmittedData transmittedData)
    {
        if (string.IsNullOrEmpty(textFromUser))
        {
            return new BotTextMessage("Ошибка ввода номера кабинета. Введите число.");
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

        if (textFromUser.Length > 100)
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
            transmittedData.State = State.WaitingPhoto;

            textFromUser = "Отправьте фото, чтобы прикрепить его к заявке";

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

            transmittedData.State = State.WaitingDataVerification;

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

    #region отправка заявки в API + получение id заявки с API + если нажади "отмена" переходим в начало подачи заявки

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

            //string imageUrl = (string)transmittedData.DataStorage.Get("imageUrl");

            if (textFromUser.Equals(InlineButtonsStorage.YesSendPhoto.CallBackData))
            {
                _applicationEntity = new ApplicationEntity()
                {
                    UserId = (long)transmittedData.DataStorage.Get("userId"),
                    AddressId = (int)transmittedData.DataStorage.Get("addressId"),
                    NumberCabinet = (string)transmittedData.DataStorage.Get("cabinetNumber"),
                    FullName = (string)transmittedData.DataStorage.Get("fullName"),
                    NumberPhone = (string)transmittedData.DataStorage.Get("numberPhone"),
                    DescriptionProblem = (string)transmittedData.DataStorage.Get("descriptionProblem"),
                    Photo = (string)transmittedData.DataStorage.Get("imageUrl")
                };

                _applicationApiWorker.AddNewApplication(_applicationEntity);

                textFromUser =
                    $"UserId: {_applicationEntity.UserId} \nAddressId: {_applicationEntity.AddressId}, \nnumber cabinet: {_applicationEntity.NumberCabinet}, \nfullname: {_applicationEntity.FullName}, \nnumber phone: {_applicationEntity.NumberPhone}, \ndescription problem: {_applicationEntity.DescriptionProblem} \nurl photo: {_applicationEntity.Photo}";

                transmittedData.DataStorage.Clear();
            }
            else
            {
                _applicationEntity = new ApplicationEntity()
                {
                    UserId = (long)transmittedData.DataStorage.Get("userId"),
                    AddressId = (int)transmittedData.DataStorage.Get("addressId"),
                    NumberCabinet = (string)transmittedData.DataStorage.Get("cabinetNumber"),
                    FullName = (string)transmittedData.DataStorage.Get("fullName"),
                    NumberPhone = (string)transmittedData.DataStorage.Get("numberPhone"),
                    DescriptionProblem = (string)transmittedData.DataStorage.Get("descriptionProblem"),
                    Photo = (string)transmittedData.DataStorage.Get("isNoPhoto")
                };

                _applicationApiWorker.AddNewApplication(_applicationEntity);

                textFromUser =
                    $"UserId: {_applicationEntity.UserId} \nAddressId: {_applicationEntity.AddressId}, \nnumber cabinet: {_applicationEntity.NumberCabinet}, \nfullname: {_applicationEntity.FullName}, \nnumber phone: {_applicationEntity.NumberPhone}, \ndescription problem: {_applicationEntity.DescriptionProblem} \nurl photo: {_applicationEntity.Photo}";

                transmittedData.DataStorage.Clear();
            }
            
            //long userId = (long)transmittedData.DataStorage.Get("userId");
            //_applicationApiWorker.GetByIdApplication(userId);

            // textFromUser = $"Заявка {application.Id} успешно создана! Вам придет уведомление, когда статус заявки будет изменен.";

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