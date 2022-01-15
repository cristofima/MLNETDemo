using Microsoft.Extensions.ML;
using MLDemo.WebApi.DataModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

builder.Services.AddPredictionEnginePool<SentimentAnalysisModelInput, SentimentAnalysisModelOutput>()
    .FromFile(modelName: "MLDemo.Models.SentimentAnalysisMLModel", filePath: "MLModels/SentimentAnalysisMLModel.zip", watchForChanges: true);

builder.Services.AddPredictionEnginePool<SMSSpamDetectionModelInput, SMSSpamDetectionModelOutput>()
    .FromFile(modelName: "MLDemo.Models.SMSSpamDetectionMLModel", filePath: "MLModels/SMSSpamDetectionMLModel.zip", watchForChanges: true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());

app.MapControllers();

app.Run();
