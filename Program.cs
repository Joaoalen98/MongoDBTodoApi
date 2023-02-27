using MongoDBTodoApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TodoDbSettings>(
    builder.Configuration.GetSection("TodoDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
