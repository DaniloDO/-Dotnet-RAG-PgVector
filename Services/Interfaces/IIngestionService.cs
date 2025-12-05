using System;

namespace AiIntegratedApp.Services.Interfaces;

public interface IIngestionService
{
    Task IngestAsync(string content); 
    Task<int> IngestManualDatabaseAsync(); 
}
