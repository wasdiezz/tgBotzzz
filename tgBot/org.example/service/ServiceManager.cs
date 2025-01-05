using tgBot.org.example.statemachine;
using tgBot.org.example.service.logic;

namespace tgBot.org.example.service;

public class ServiceManager
{
    private Dictionary<State, Func<string, TransmittedData, BotTextMessage>> _methods;

    private StartLogic _startLogic;
    private FaqLogic _faqLogic;
    private ApplicationLogic _applicationLogic;
    private HistoryLogic _historyLogic;


    public ServiceManager()
    {
        _methods = new Dictionary<State, Func<string, TransmittedData, BotTextMessage>>();

        _startLogic = new StartLogic();
        _faqLogic = new FaqLogic();
        _applicationLogic = new ApplicationLogic();
        _historyLogic = new HistoryLogic();


        #region начало работы бота

        _methods.Add(State.WaitingCommandStart, _startLogic.ProcessWaitingCommandStart);

        _methods.Add(State.WaitingQuestionsOrApplicationOrHistory,
            _startLogic.ProcessWaitingQuestionsOrApplicationOrHistory);

        _methods.Add(State.WaitingQuestions, _startLogic.ProcessWaitingQuestions);

        _methods.Add(State.WaitingApplication, _startLogic.ProcessWaitingApplication);

        #endregion

        #region просмотр комп

        _methods.Add(State.WaitingViewProblemComputer, _faqLogic.ProcessWaitingViewProblemComputer);
        _methods.Add(State.WaitingFirstInfoProblemComputer, _faqLogic.ProcessWaitingFirstInfoProblemComputer);
        _methods.Add(State.WaitingSecondInfoProblemComputer, _faqLogic.ProcessWaitingSecondInfoProblemComputer);
        _methods.Add(State.WaitingThirdInfoProblemComputer, _faqLogic.ProcessWaitingThirdInfoProblemComputer);

        #endregion

        #region принтер

        _methods.Add(State.WaitingViewProblemPrinter, _faqLogic.ProcessWaitingViewProblemPrinter);
        _methods.Add(State.WaitingFirstInfoProblemPrinter, _faqLogic.ProcessWaitingFirstInfoProblemPrinter);
        _methods.Add(State.WaitingSecondInfoProblemPrinter, _faqLogic.ProcessWaitingSecondInfoProblemPrinter);

        #endregion

        #region проектор

        _methods.Add(State.WaitingViewProblemProjector, _faqLogic.ProcessWaitingViewProblemProjector);
        _methods.Add(State.WaitingFirstInfoProblemProjector, _faqLogic.ProcessWaitingFirstInfoProblemProjector);
        _methods.Add(State.WaitingSecondInfoProblemProjector, _faqLogic.ProcessWaitingSecondInfoProblemProjector);
        _methods.Add(State.WaitingThirdInfoProblemProjector, _faqLogic.ProcessWaitingThirdInfoProblemProjector);

        #endregion

        #region заявка

        _methods.Add(State.WaitingInputCabinetNumber, _applicationLogic.ProcessWaitingInputCabinetNumber);
        _methods.Add(State.WaitingInputFullName, _applicationLogic.ProcessWaitingInputFullName);
        _methods.Add(State.WaitingInputNumberPhone, _applicationLogic.ProcessWaitingInputNumberPhone);
        _methods.Add(State.WaitingDescriptionProblem, _applicationLogic.ProcessWaitingDescriptionProblem);
        _methods.Add(State.WaitingQuestionAddPhoto, _applicationLogic.ProcessWaitingQuestionAddPhoto);
        _methods.Add(State.WaitingPhoto, _applicationLogic.ProcessWaitingPhoto);
        _methods.Add(State.WaitingDataVerification, _applicationLogic.ProcessWaitingDataVerification);
        _methods.Add(State.WaitingDataVerificationWithoutPhoto, _applicationLogic.ProcessWaitingDataVerificationWithoutPhoto);
        _methods.Add(State.WaitingReadApplication, _applicationLogic.ProcessWaitingReadApplication);

        #endregion

        #region история заявок

        _methods.Add(State.WaitingShowHistory, _startLogic.ProcessWaitingQuestionsOrApplicationOrHistory);
        _methods.Add(State.WaitingFirstShowCommands, _historyLogic.ProcessWaitingShowHistory);
        _methods.Add(State.WaitingMiddleShowCommands, _historyLogic.processWaitingMiddleShowCommands);
        _methods.Add(State.WaitingLastShowCommands, _historyLogic.processWaitingLastShowCommands);

        #endregion
    }

    public BotTextMessage ProcessBotUpdate(string textData, TransmittedData transmittedData)
    {
        var serviceMethod = _methods[transmittedData.State];

        return serviceMethod.Invoke(textData, transmittedData);
    }
}