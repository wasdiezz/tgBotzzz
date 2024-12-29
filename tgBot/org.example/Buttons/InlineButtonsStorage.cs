namespace tgBot.org.example.Buttons;

public class InlineButtonsStorage
{
    public static InlineButton Back { get; } = new InlineButton("Назад", "MovePrevShow");
    public static InlineButton Next { get; } = new InlineButton("Вперед", "Next");

    public static InlineButton BackToMenu { get; } = new InlineButton("Главное меню", "BackToMenu");

    public static InlineButton ShowQuestions { get; } = new InlineButton("Частые вопросы", "ShowQuestions");    

    public static InlineButton SubmitApplication { get; } = new InlineButton("Оставить заявку", "SubmitApplication");

    public static InlineButton SubmitHistory { get; } = new InlineButton("История заявок", "SubmitHistory");

    public static InlineButton ViewProblemComputer { get; } = new InlineButton("Компьютер", "ViewProblemComputer");
    public static InlineButton ViewProblemPrinter { get; } = new InlineButton("Принтер", "ViewProblemPrinter");
    public static InlineButton ViewProblemProjector { get; } = new InlineButton("Проектор", "ViewProblemProjector");


    public static InlineButton FirstInfo { get; } = new InlineButton("1", "FirstInfo");
    public static InlineButton SecondInfo { get; } = new InlineButton("2", "SecondInfo");
    public static InlineButton ThirdInfo { get; } = new InlineButton("3", "ThirdInfo");

    public static InlineButton YesSendPhoto { get; } = new InlineButton("Да", "YesSendPhoto");
    public static InlineButton NoSendPhoto { get; } = new InlineButton("Нет", "NoSendPhoto");
    
    public static InlineButton SendApplication { get; } = new InlineButton("Отправить", "SendApplication");
    public static InlineButton CancelApplication { get; } = new InlineButton("Отменить", "CancelApplication");


    public static InlineButton FirstAddressPlace { get; } = new InlineButton("Свобода 33", "FirstAddressPlace");
    public static InlineButton SecondAddressPlace { get; } = new InlineButton("Лодочная 7", "SecondAddressPlace");
    public static InlineButton ThirdAddressPlace { get; } = new InlineButton("Молдавская 5", "ThirdAddressPlace");
    public static InlineButton FourAddressPlace { get; } = new InlineButton("Просп. Буденного 35", "FourAddressPlace");
    public static InlineButton FiveAddressPlace { get; } =
        new InlineButton("Волгоградский проспект 42 корпус 5.", "FiveAddressPlace");
}