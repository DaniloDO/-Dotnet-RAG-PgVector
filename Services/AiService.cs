using System;
using AiIntegratedApp.Services.Interfaces;
using Microsoft.Extensions.AI;

namespace AiIntegratedApp.Services;

public class AiService : IAiService
{
    private readonly IChatClient _chatClient; 

    public AiService(IChatClient chatClient)
    {
        _chatClient = chatClient; 
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        ChatResponse response = await _chatClient.GetResponseAsync(prompt); 

        string text = response.Messages
            .SelectMany(m => m.Contents)
            .OfType<TextContent>()
            .Select(c => c.Text)
            .FirstOrDefault() ?? string.Empty; 

        return text; 
    }
}
