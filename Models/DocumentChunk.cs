using System;
using Pgvector; 

namespace AiIntegratedApp.Models;

public class DocumentChunk
{
    public int Id { get; set; } 
    public string Content { get; set; } = string.Empty; 

    public Vector Embedding { get; set; } = default!;  
}
