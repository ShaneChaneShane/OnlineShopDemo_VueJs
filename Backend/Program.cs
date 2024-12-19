using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);
Env.Load();
string frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:8080"; // Default fallback

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(frontendUrl) // Frontend URL
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Enable CORS policy
app.MapControllers();

app.Run();
