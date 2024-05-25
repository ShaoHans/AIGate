
using Flurl.Http;
using Flurl.Http.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using OpenAIClient;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddSingleton(sp =>
{
    return new FlurlClientCache()
    .Add("OpenAI", "https://api.openai.com", clientBuilder =>
    {
        clientBuilder.WithOAuthBearerToken("sk-proj-xxx");
    });
});
builder.Services.AddTransient<OpenAIService>();

var host = builder.Build();
var aiService = host.Services.GetRequiredService<OpenAIService>();
await aiService.ChatCompletionAsync();

await host.RunAsync();