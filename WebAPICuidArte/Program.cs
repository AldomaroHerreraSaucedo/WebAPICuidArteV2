using Microsoft.EntityFrameworkCore;
using WebAPICuidArte.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conexionString = builder.Configuration.GetConnectionString("ConexionSqlServer");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<BDContexto>(opcion => opcion.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSqlServer")));
builder.Services.AddDbContext<BDContexto>(options => options.UseSqlServer(conexionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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

app.UseAuthorization();

app.MapControllers();

app.Run();
