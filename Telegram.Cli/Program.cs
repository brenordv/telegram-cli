using Telegram.Cli.Clients;
using Telegram.Cli.Extensions;
using Telegram.Cli.Helpers;

var message = args.ExtractMessage();
if (string.IsNullOrWhiteSpace(message))
    message = Console.In.ReadToEnd();
    
if (string.IsNullOrWhiteSpace(message))
{
    Console.WriteLine("No message found");
    Environment.Exit(1);
}

var config = ConfigLoader.LoadConfig();
await new TelegramClient(config, new HttpClient()).SendMessageAsync(message);