using ItsRandomLife.Domain.Interfaces;
using ItsRandomLife.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MainConnectionString");

// Настройка Serilog для логирования
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Добавление сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация репозиториев
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IDailyPhraseRepository, DailyPhraseRepository>();

var app = builder.Build();

// Конфигурация HTTP-пайплайна
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();