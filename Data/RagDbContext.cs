using System;
using AiIntegratedApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pgvector.EntityFrameworkCore; 

namespace AiIntegratedApp.Data;

/*
Databases need to have a vector column to store the embedding representations.
The size of that vector has to match the one used in the embedding model to implement.
The nomic-embed-text model has a 768-dimensional embedding output and supports a 8192-token context window.

After the database is created create an indexing search method to optimize future searches,
HNSW (Hierarchical Navigable Small World) is currently the best alternative provided by PostgreSQL extension

CREATE INDEX idx_chunks_embedding_hnsw
ON "Chunks"
USING hnsw ("Embedding" vector_l2_ops)
WITH (m=16, ef_construction=200);   
*/   

public class RagDbContext : IdentityDbContext
{
    public DbSet<DocumentChunk> Chunks => Set<DocumentChunk>();

    public RagDbContext(DbContextOptions<RagDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector"); 
        
        modelBuilder.Entity<DocumentChunk>(entity => 
        {
            entity.Property(e => e.Embedding)
                .HasColumnType("vector(768)"); 
        });

        base.OnModelCreating(modelBuilder); 
    }
}
