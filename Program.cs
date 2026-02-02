using APIGestionNotas.Managers;

var builder = WebApplication.CreateBuilder(args);

// Agrego dependencias
builder.Services.AddSingleton<INotaManager, NotaManager>();

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
