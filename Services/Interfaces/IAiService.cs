using System;
using Microsoft.Extensions.AI;

namespace AiIntegratedApp.Services.Interfaces;

public interface IAiService
{
    Task<string> GenerateAsync(string prompt); 
}
