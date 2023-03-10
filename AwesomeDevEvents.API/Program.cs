using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//cria a conection string
var cnn = builder.Configuration.GetConnectionString("DevEventsCS");

////adicionando Bd Fake/ simulando um bd em memória
//builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));

//inclui conexão com banco sql
builder.Services.AddDbContext<DevEventsDbContext>(db => db.UseSqlServer(cnn));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//configuração do Swagger, personalização
builder.Services.AddSwaggerGen(sw =>
{
    sw.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AwesomeDevEvents.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Email = "betoxz@gmail.com", //pode usar por exemplo o email do grupo da equipe desenvolvedora,
            Name = "Carlos Martins",
            Url = new Uri("http://enderecodosite.com.br")
        }       
    });

    //caminho do arquivo de documentação
    var xmlFile = "AwesomeDevEvents.API.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sw.IncludeXmlComments(xmlPath);

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
