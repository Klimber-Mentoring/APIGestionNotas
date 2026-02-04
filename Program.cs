using APIGestionNotas;
using APIGestionNotas.Managers;
using AutoMapper;
using APIGestionNotas.Helpers;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Agrego dependencias
var configAutomapper = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfileConfiguration());
},null);

var mapper = configAutomapper.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddSingleton<INotaManager, NotaManager>();
builder.Services.AddSingleton<Inicializador>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

