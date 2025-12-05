using AiIntegratedApp.Data;
using AiIntegratedApp.Services;
using AiIntegratedApp.Services.Interfaces;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI; 

using AiIntegratedApp.Models; 

var builder = WebApplication.CreateBuilder(args);

//Load env variables
Env.Load();

//Build connection string and add DbContext
var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var database = Environment.GetEnvironmentVariable("POSTGRES_DATABASE");
var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
var password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

//Set Ollama URI and model
var ollamaURI = Environment.GetEnvironmentVariable("OLLAMA_PORT");
var ollamaModel = Environment.GetEnvironmentVariable("OLLAMA_MODEL"); 
var ollamaEmbeddingModel = Environment.GetEnvironmentVariable("OLLAMA_EMBEDDING_MODEL"); 

//create connection string
var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}"; 

//Add DbContext and connection string
builder.Services.AddDbContext<RagDbContext>(options => 
    options.UseNpgsql(
        connectionString,
        npgsqlOptions => npgsqlOptions.UseVector()
    )
); 

//Configure IdentityCore
builder.Services.AddIdentityCore<User>(options => 
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false; 
})
.AddEntityFrameworkStores<RagDbContext>(); 

//Create embedding generator
var embeddingGenerator = new OllamaEmbeddingGenerator(new Uri(ollamaURI), ollamaEmbeddingModel); 

//Configure embedding generator
builder.Services.AddEmbeddingGenerator<string, Embedding<float>>(embeddingGenerator); 

//Configure AI chat client
builder.Services.AddChatClient(new OllamaChatClient(new Uri(ollamaURI), ollamaModel)); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAiService, AiService>();
builder.Services.AddScoped<IIngestionService, IngestionService>();
builder.Services.AddScoped<IRAGService, RAGService>(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers(); 

app.MapGet("/", () => $"Hello {ollamaModel}!");

app.Run();
