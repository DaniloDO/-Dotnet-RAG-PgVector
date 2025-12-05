using System;
using AiIntegratedApp.DTOs;
using AiIntegratedApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

using AiIntegratedApp.DTOs; 

namespace AiIntegratedApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiController : ControllerBase
{
    private readonly IAiService _aiService; 
    private readonly IRAGService _ragService;
    private readonly IIngestionService _ingestionService; 

    public AiController(IAiService aiService, IRAGService ragService, IIngestionService ingestionService)
    {
        _aiService = aiService;
        _ragService = ragService;
        _ingestionService = ingestionService; 
    }

    [HttpPost("generate")]
    public async Task<ActionResult> Generate([FromBody] PromptRequestDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Prompt))
        {
            return BadRequest("Prompt can't be empty"); 
        }

        var response = await _aiService.GenerateAsync(dto.Prompt);

        return Ok(new {response}); 
    }

    [HttpPost("ingest")]
    public async Task<ActionResult> IngestManualData()
    {
        int count = await _ingestionService.IngestManualDatabaseAsync(); 
        return Ok(new {inserted = count}); 
    }

    [HttpPost("rag")]
    public async Task<ActionResult> RAGGenerate([FromBody] PromptRequestDTO dto)
    {
        if (string.IsNullOrEmpty(dto.Prompt))
        {
            return BadRequest("Prompt can't be empty"); 
        }

        var response = await _ragService.RAGGenerateAsync(dto.Prompt);

        return Ok(new {response});    
    }

}
