using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPICuidArte.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexionString = builder.Configuration.GetConnectionString("ConexionSqlServer");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuración de Swagger para autenticación mediante API Key
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Ingrese su API Key para acceder a la API",
        Name = "X-API-KEY",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" },
                Scheme = "ApiKeyScheme",
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<BDContexto>(options => options.UseSqlServer(conexionString));

var app = builder.Build();

/*
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

// Proteger Swagger con Basic Auth
var swaggerUser = builder.Configuration["SwaggerAuth:User"];
var swaggerPass = builder.Configuration["SwaggerAuth:Password"];

app.UseWhen(context => context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        string authHeader = context.Request.Headers["Authorization"];

        if (authHeader == null || !authHeader.StartsWith("Basic "))
        {
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = 401;
            return;
        }

        var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
        var encoding = System.Text.Encoding.GetEncoding("iso-8859-1");
        var usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

        var parts = usernamePassword.Split(':');
        var username = parts[0];
        var password = parts[1];

        if (username != swaggerUser || password != swaggerPass)
        {
            context.Response.StatusCode = 401;
            return;
        }

        await next();
    });
});

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var servicio = scope.ServiceProvider;
    var contexto = servicio.GetRequiredService<BDContexto>();
    contexto.Database.EnsureCreated();

    // Cargar los datos de inicio
    BDInicializa.Inicializar(contexto);
}

app.UseHttpsRedirection();

// API KEY
var apiKey = builder.Configuration["ApiKeySettings:ApiKey"];

// Middleware para validar API KEY
app.Use(async (context, next) =>
{
    // Solo proteger rutas que empiecen con /api
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        // Permitir acceso al login
        if (context.Request.Path.StartsWithSegments("/api/InicioSesion"))
        {
            await next();
            return;
        }

        var apiKeyHeader = context.Request.Headers["X-API-KEY"].FirstOrDefault();

        if (apiKeyHeader == null || apiKeyHeader != apiKey)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }
    }

    await next();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
