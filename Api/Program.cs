using Application.Interfaces;
using Application.Services;
using Domain.Contracts;
using Infrastructure.Clients;
using Infrastructure.Mongo;
using Infrastructure.Repositories;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

var jwtKey = jwtSettings["Key"];
if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException("JWT Key no está configurada.");
}

var key = Encoding.UTF8.GetBytes(jwtKey);

// Add services to the container.
builder.Services.AddControllers();

// 1. Agrega los exploradores de endpoints (necesario para Swagger)
builder.Services.AddEndpointsApiExplorer();

// 2. Registra el generador de Swagger
builder.Services.AddSwaggerGen(options =>
{
    // ... tu código existente de XML ...
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);

});

// Mongo Settings
builder.Services.Configure<MongoSettings>(
    builder.Configuration.GetSection("MongoSettings")
);

builder.Services.AddHttpClient<ICatApiClientRepository, CatApiClientRepository>(client =>
{
    var config = builder.Configuration.GetSection("CatApi");

    client.BaseAddress = new Uri(config["BaseUrl"]!);
    client.DefaultRequestHeaders.Add("x-api-key", config["ApiKey"]);
});



builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<
        Microsoft.Extensions.Options.IOptions<MongoSettings>>().Value
);

builder.Services.AddSingleton<MongoContext>();

// Dependency Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddHttpClient<ICatApiClientRepository, CatApiClientRepository>();
builder.Services.AddScoped<ICatServiceApiClient, CatServiceClient>();



builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Prueba Api V1");
    });
    app.UseDeveloperExceptionPage();
}

app.UseRouting();
app.MapControllers();


app.Run();