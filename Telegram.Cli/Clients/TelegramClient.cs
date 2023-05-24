using System.Text;
using System.Text.Json;
using Telegram.Cli.Extensions;
using Telegram.Cli.ValueObjects;

namespace Telegram.Cli.Clients;

public class TelegramClient
{
    private readonly RuntimeConfig _config;
    private readonly HttpClient _httpClient;
    private const int MaxMessageSize = 4096;
    private const string MediaType = "application/json";
    
    public TelegramClient(RuntimeConfig config, HttpClient httpClient)
    {
        _config = config;
        _httpClient = httpClient;
    }
    
    public async Task SendMessageAsync(string message)
    {
        foreach (var messagePart in message.Split(MaxMessageSize))
        {
            await SendMessageRequestAsync(messagePart);
        }
    }

    private async Task SendMessageRequestAsync(string message)
    {
        var requestUrl = $"https://api.telegram.org/bot{_config.ApiKey}/sendMessage";

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            chat_id = _config.ChatId,
            text = message
        }), Encoding.UTF8, MediaType);

        try
        {
            var response = await _httpClient.PostAsync(requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error occurred when sending message: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred when sending message: {ex.Message}");
        }
    }
}