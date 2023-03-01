using CodeChallenge.Application.Mapping;
using CodeChallenge.Domain.Settings;
using CodeChallenge.Infrastructure.EntityFramework;
using CodeChallenge.Infrastructure.HttpHandlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddAutoMapper(config =>
{
    config.AllowNullDestinationValues = true;
    config.AllowNullCollections = true;
    config.AddMaps(Assembly.GetAssembly(typeof(ProductMapper)));
});

builder.Services.AddHttpClient("CodeChallengeLogin", ConfigureCodeChallengeHttpClient);

builder.Services.AddHttpClient("CodeChallenge", ConfigureCodeChallengeHttpClient)
    .AddHttpMessageHandler<CodeChallengeLoginHandler>();

builder.Services.AddDbContext<CodeChallengeContext>(x =>
    x.UseInMemoryDatabase(databaseName: "CodeChallengeDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

CreateDatabase(app);

app.Run();

static void ConfigureCodeChallengeHttpClient(IServiceProvider sp, HttpClient httpClient)
{
    var settings = sp.GetRequiredService<IOptions<CodeChallengeApiSettings>>().Value;
    httpClient.BaseAddress = new Uri(settings.BaseUrl);
}

static void CreateDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<CodeChallengeContext>();

    context.Database.EnsureCreated();
}
