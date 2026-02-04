using APIGestionNotas;
using APIGestionNotas.Helpers;
using APIGestionNotas.Managers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Agrego dependencias
ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });

/* Cree uno vacío ya que era la única manera de no obtener una excepción NullReferenceException
 en tiempo de ejcución. Permite definir cómo y dónde se guardan los logs en aplicaciones  */

var configAutomapper = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfileConfiguration());
}, loggerFactory);

var mapper = configAutomapper.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddSingleton<INotaManager, NotaManager>();
builder.Services.AddSingleton<Inicializador>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Incorporo comentarios XML
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Notas API", Version = "v1" });

    // Include XML comments
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//no corria https, asi que:
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7041, o => o.UseHttps());
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var inicializador = scope.ServiceProvider.GetRequiredService<Inicializador>();
    inicializador.CargarDatosPrueba();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

