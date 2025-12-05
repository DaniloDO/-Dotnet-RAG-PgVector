using System;

namespace AiIntegratedApp.Services.Interfaces;

public interface IRAGService
{
    Task<string> RAGGenerateAsync(string question); 
}
