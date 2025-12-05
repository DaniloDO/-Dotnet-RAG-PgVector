using System;
using AiIntegratedApp.Data;
using AiIntegratedApp.Models;
using AiIntegratedApp.SeedData;
using AiIntegratedApp.Services.Interfaces;
using Microsoft.Extensions.AI;

namespace AiIntegratedApp.Services;

public class IngestionService : IIngestionService
{
    private readonly RagDbContext _database; 
    private readonly IEmbeddingGenerator<string, Embedding<float>> _embed; 

    public IngestionService(RagDbContext database, IEmbeddingGenerator<string, Embedding<float>> embed)
    {
        _database = database; 
        _embed = embed; 
    }

    public async Task IngestAsync(string content)
    {
        var embedding = await _embed.GenerateAsync(content);
        var vector = new Pgvector.Vector(embedding.Vector.ToArray()); 

        _database.Chunks.Add(new DocumentChunk
        {
            Content = content,
            Embedding = vector
        });

        await _database.SaveChangesAsync(); 
    }

    public async Task<int> IngestManualDatabaseAsync()
    {
        foreach (var text in SeedIngest.TopicContextBase)
        {
            var embedding = await _embed.GenerateAsync(text);
            var vector = new Pgvector.Vector(embedding.Vector.ToArray());  

            var chunk = new DocumentChunk
            {
                Content = text,
                Embedding = vector
            };

            _database.Chunks.Add(chunk);  
        }

        return await _database.SaveChangesAsync(); 
    }
}
