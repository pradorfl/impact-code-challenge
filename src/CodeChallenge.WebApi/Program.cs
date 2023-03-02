using CodeChallenge.Application.Mapping;
using CodeChallenge.Application.Services;
using CodeChallenge.Application.Services.Interfaces;
using CodeChallenge.Domain.Clients;
using CodeChallenge.Domain.Repositories;
using CodeChallenge.Domain.Services;
using CodeChallenge.Domain.Services.Interfaces;
using CodeChallenge.Domain.Settings;
using CodeChallenge.Infrastructure.Clients;
using CodeChallenge.Infrastructure.EntityFramework;
using CodeChallenge.Infrastructure.HttpHandlers;
using CodeChallenge.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<CodeChallengeApiSettings>(
    builder.Configuration.GetSection(nameof(CodeChallengeApiSettings)));

builder.Services.Configure<GeneralSettings>(
    builder.Configuration.GetSection(nameof(GeneralSettings)));

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

RegisterServices(builder.Services);

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

static void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IBasketAppService, BasketAppService>();
    services.AddScoped<IProductAppService, ProductAppService>();
    services.AddScoped<IBasketService, BasketService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<IBasketRepository, BasketRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<ICodeChallengeApiClient, CodeChallengeApiClient>();
    services.AddScoped<CodeChallengeLoginHandler>();
}

public partial class Program { }