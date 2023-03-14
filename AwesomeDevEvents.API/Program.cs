using AwesomeDevEvents.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);


builder.Logging.ClearProviders();

//cria a conection string
var cnn = builder.Configuration.GetConnectionString("DevEventsCS");

// Use Serilog
builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration
        .Enrich.FromLogContext()
        .WriteTo.MSSqlServer(cnn, sinkOptions: new MSSqlServerSinkOptions
        {
            AutoCreateSqlTable = true,
            TableName = "LogApiDevEvents"
        })
        .WriteTo.Console(Serilog.Events.LogEventLevel.Debug);
});



////adicionando Bd Fake/ simulando um bd em mem�ria
//builder.Services.AddDbContext<DevEventsDbContext>(o => o.UseInMemoryDatabase("DevEventsDb"));

//inclui conex�o com banco sql
builder.Services.AddDbContext<DevEventsDbContext>(db => db.UseSqlServer(cnn));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//configura��o do Swagger, personaliza��o
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

    //caminho do arquivo de documenta��o
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
