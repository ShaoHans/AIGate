using Flurl.Http;
using Flurl.Http.Configuration;

namespace OpenAIClient;

internal class OpenAIService
{
    private readonly IFlurlClient _client;

    public OpenAIService(IFlurlClientCache clientCache)
    {
        _client = clientCache.Get("OpenAI");
    }

    public async Task ChatCompletionAsync()
    {
        var result = await _client.Request("/v1/chat/completions").PostJsonAsync(new
        {
            model = "gpt-3.5-turbo-16k",
            messages = new[]
            {
                new{role="system",content="你是一位非常优秀的AI助手" },
                new{role="user",content="中国有多少年的历史文化" },
            }
        });
        var test = await result.GetStringAsync();
    }
}
