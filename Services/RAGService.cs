using System;
using AiIntegratedApp.Data;
using AiIntegratedApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Pgvector.EntityFrameworkCore;

namespace AiIntegratedApp.Services;

public class RAGService : IRAGService
{
    private readonly IChatClient _chatClient;
    private readonly RagDbContext _database;
    private readonly IEmbeddingGenerator<string, Embedding<float>> _embed;

    public RAGService(
        IChatClient chatClient,
        RagDbContext database,
        IEmbeddingGenerator<string, Embedding<float>> embed
    )
    {
        _chatClient = chatClient;
        _database = database;
        _embed = embed;
    }

    public async Task<string> RAGGenerateAsync(string question)
    {
        var questionEmbed = await _embed.GenerateAsync(question);
        var vector = new Pgvector.Vector(questionEmbed.Vector.ToArray());

        var neighbors = await _database.Chunks
            .OrderBy(c => c.Embedding!.L2Distance(vector))
            .Take(5)
            .ToListAsync();

        var context = string.Join("\n", neighbors.Select(n => n.Content));

        var prompt = $"""
        Use the following context to answer the question.

        CONTEXT:
        {context}

        QUESTION:
        {question}
        """;

        var response = await _chatClient.GetResponseAsync(prompt); 

        string text = response.Messages
            .SelectMany(m => m.Contents)
            .OfType<TextContent>()
            .Select(c => c.Text)
            .FirstOrDefault() ?? string.Empty; 

        return text; 
    }


}
