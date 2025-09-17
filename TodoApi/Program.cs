using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

// NSwag: generate an OpenAPI doc
builder.Services.AddOpenApiDocument(settings =>
{
    settings.Title = "My API";
    settings.Version = "v1";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Serve the OpenAPI JSON (you can choose the path)
    app.UseOpenApi(opt => opt.Path = "/openapi/v1.json");

    // Serve Swagger UI (v3) and point it to that JSON
    app.UseSwaggerUi(opt =>
    {
        opt.Path = "/swagger";                // UI at /swagger
        opt.DocumentPath = "/openapi/v1.json"; // JSON at /openapi/v1.json
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
