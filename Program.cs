using MSilvaPizza.Models;
using MSilvaPizza.Services;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=Pizzas.db";

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        policy.WithOrigins("http://localhost:5173").WithMethods("GET", "POST").AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configurar pasta especifica de migrations para cada tabela
builder.Services.AddSqlite<PizzaDb>(connectionString, sqliteOptions =>
{
    sqliteOptions.MigrationsHistoryTable("__EFMigrationsHistory_Pizza");
});
builder.Services.AddSqlite<UserDb>(connectionString, sqliteOptions =>
{
    sqliteOptions.MigrationsHistoryTable("__EFMigrationsHistory_User");
});

builder.Services.AddScoped<IPizzaService, PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
