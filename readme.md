# Dotnet-RAG-PgVector

Retrieval-Augmented Generation API using .NET, PostgreSQL, pgVector, and Ollama.

This project is a **.NET 9 Web API** integrating **local LLMs (via Ollama)** with **RAG (Retrieval-Augmented Generation)** using **pgVector** inside PostgreSQL to store and search document embeddings. It demonstrates a practical, production-oriented architecture for building AI-enhanced APIs that combine traditional backend engineering with modern AI capabilities.

## Features

### AI Integration

- Local inference using Ollama (e.g., Phi3, LLaMA, Mistral)
- Embedding generation using Microsoft.Extensions.AI
- Modular architecture for model swapping

### RAG Pipeline
- Document ingestion service
- Chunk generation and embedding
- pgVector similarity search
- Context assembly for LLM inputs
- Clean separation between ingestion and query responsibilities

### .NET API Architecture

- ASP.NET Core 9 Web API
- Dependency Injection for AI, database, and vector search services
- Controller-based endpoints for ingestion & AI queries

### PostgreSQL + pgVector

- Vector storage with pgvector extension
- Uses Pgvector.EntityFrameworkCore for EF Core integration
- Supports ANN indexes (IVFFLAT / HNSW) for fast similarity search
- SQL schema generated automatically by EF

## Architecture Overview

### 1. Document Ingestion
The ingestion service splits text into chunks, generates embeddings via Ollama, and stores them in PostgreSQL:
- Chunk text
- Generate embeddings
- Save chunks + vectors

### 2. Vector Search
At query time:
- User sends prompt
- System embeds the user query
- Performs vector similarity search (cosine/Euclidean)
- Retrieves the top N relevant chunks

### 3. RAG Completion
- Retrieved context is prefixed to the prompt
- LLM generates a contextual answer
- Controller returns structured response

## Ingesting Manual Data
Use this endpoint to load test context:
```bash
POST /api/ai/ingest
```
Your manual dataset is stored in ContextBase and chunked dynamically.

## RAG Endpoint
```bash
POST /api/ai/rag
{
  "prompt": "What is the Bitcoin market outlook for 2025?"
}
```
Response:
```bash
{
  "answer": "...",
  "contextUsed": [...],
  "tokens": 1428
}
```
## Requirements

- .NET 9
- PostgreSQL 16+
- pgvector extension
- Ollama installed locally
- Tested Models:
  phi3:mini (or similar LLM)
  nomic-embed-text (or any embedding model)

## Installation
### 1. Enable pgVector
```bash
CREATE EXTENSION IF NOT EXISTS vector;
```

### 2. Run Migrations
```bash
dotnet ef database update
```

### 3. Install Ollama Models
```bash
ollama pull phi3:mini
ollama pull nomic-embed-text
```

### 4. Run the API
```bash
dotnet run
```
