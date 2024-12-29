namespace ConsoleAppTetsBot.org.example.Buttons;

public class InlineButton
{
    public string Name { set; get; }
    public string CallBackData { set; get; }

    public InlineButton(string name, string callBackData)
    {
        Name = name;
        CallBackData = callBackData;
    }
}