namespace tgBot.org.example.statemachine;

public class TransmittedData
{
    public State State { get; set; }
    public DataStorage DataStorage { get; }
    public long ChatId { get; }

    public TransmittedData(long chatId)
    {
        ChatId = chatId;
        State = State.WaitingCommandStart;
        DataStorage = new DataStorage();
    }
}