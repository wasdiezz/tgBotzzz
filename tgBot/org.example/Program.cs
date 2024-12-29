 using ConsoleAppTetsBot;

 Bot bot = new Bot();
 bot.Start();

 Console.WriteLine($"Bot @{bot.GetBotName()} started");

 Console.WriteLine("Press <Enter> for stop");
 Console.ReadKey();

 bot.Stop();
 Console.WriteLine("Bot stopped");
 