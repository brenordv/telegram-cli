using FluentAssertions;
using Telegram.Cli.Helpers;

namespace Telegram.Cli.Test.Helpers;

public class ConfigLoaderTests
{
    [Fact]
    public void LoadConfig_ShouldReturnRuntimeConfig()
    {
        // Arrange
        const string token = "token";
        const string chatId = "chatId";
        Environment.SetEnvironmentVariable("TELEGRAM_API_TOKEN", token);
        Environment.SetEnvironmentVariable("TELEGRAM_CHAT_ID", chatId);
        
        // Act
        var config = ConfigLoader.LoadConfig();
        
        // Assert
        config.Should().NotBeNull();
        config.ApiKey.Should().Be(token);
        config.ChatId.Should().Be(chatId);
    }
    
    [Fact]
    public void LoadConfig_ShouldThrowExceptionWhenTokenIsNull()
    {
        // Arrange
        const string chatId = "chatId";
        Environment.SetEnvironmentVariable("TELEGRAM_API_TOKEN", null);
        Environment.SetEnvironmentVariable("TELEGRAM_CHAT_ID", chatId);
        
        // Act
        Action act = () => ConfigLoader.LoadConfig();
        
        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'TELEGRAM_API_TOKEN')");
    }
    
    [Fact]
    public void LoadConfig_ShouldThrowExceptionWhenChatIdIsNull()
    {
        // Arrange
        const string token = "token";
        Environment.SetEnvironmentVariable("TELEGRAM_API_TOKEN", token);
        Environment.SetEnvironmentVariable("TELEGRAM_CHAT_ID", null);
        
        // Act
        Action act = () => ConfigLoader.LoadConfig();
        
        // Assert
        act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'TELEGRAM_CHAT_ID')");
    }
}