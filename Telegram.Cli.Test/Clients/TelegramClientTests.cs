using System.Net;
using FluentAssertions;
using Moq;
using Moq.Protected;
using Telegram.Cli.Clients;
using Telegram.Cli.ValueObjects;

namespace Telegram.Cli.Test.Clients;

public class TelegramClientTests
{
    private const string TestApiKey = "testApiKey";
    private const string TestChatId = "123456789";
    private const string TestMessage = "Hello, Telegram!";
    private const string BaseUrl = "https://api.telegram.org/bot";

    [Fact]
    public async Task SendMessageAsync_WhenCalled_ShouldSendCorrectRequest()
    {
        // Arrange
        var config = new RuntimeConfig(TestApiKey, TestChatId);
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            })
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(BaseUrl),
        };

        var telegramClient = new TelegramClient(config, httpClient);

        // Act
        await telegramClient.SendMessageAsync(TestMessage);

        // Assert
        var expectedUri = new Uri($"{BaseUrl}{TestApiKey}/sendMessage");
        handlerMock.Protected().Verify(
            "SendAsync",
            Times.Exactly(1),
            ItExpr.Is<HttpRequestMessage>(req =>
                req.Method == HttpMethod.Post
                && req.RequestUri == expectedUri
                && req.Content.ReadAsStringAsync().Result.Contains(TestMessage)
            ),
            ItExpr.IsAny<CancellationToken>()
        );
    }

    [Fact]
    public async Task SendMessageAsync_WhenResponseIsFailure_ShouldLogError()
    {
        // Arrange
        var config = new RuntimeConfig(TestApiKey, TestChatId);
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        const string expectedErrorMessage = "An error occurred";

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(expectedErrorMessage),
            })
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(BaseUrl),
        };

        var telegramClient = new TelegramClient(config, httpClient);

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await telegramClient.SendMessageAsync(TestMessage);

        // Assert
        consoleOutput.ToString().Should().Contain(expectedErrorMessage);
    }

    [Fact]
    public async Task SendMessageAsync_WhenExceptionOccurs_ShouldLogException()
    {
        // Arrange
        var config = new RuntimeConfig(TestApiKey, TestChatId);
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        const string expectedExceptionMessage = "Exception was thrown";

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Throws(new Exception(expectedExceptionMessage))
            .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri(BaseUrl),
        };

        var telegramClient = new TelegramClient(config, httpClient);

        var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        await telegramClient.SendMessageAsync(TestMessage);

        // Assert
        consoleOutput.ToString().Should().Contain(expectedExceptionMessage);
    }
}