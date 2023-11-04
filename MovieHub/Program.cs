using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieHub.Data;
using MovieHub.Data.Models;
using MovieHub.Data.Repositories;
using MovieHub.Data.Repositories.Interfaces;
using MovieHub.Queries;
using MovieHub.Mutations;
using MovieHub.Utilities.Configurations;
using MovieHub.Utilities.Configurations.Collections;
using System.Text.Json;
using MovieHub.Types.User;
using MovieHub.Utilities.Services;
using MovieHub.Transport;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddHttpRequestInterceptor<AuthenticationInterceptor>()
    .AddMutationConventions()
    .AddMutationType<Mutation>()
    .AddQueryType<ShowQuery>()
    .RegisterService<IUserRepository>()
    .RegisterService<IShowRepository>()
    .RegisterService<IAuthenticationService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<BaseDataConfiguration>(
    configuration.GetSection(nameof(BaseDataConfiguration)));

builder.Services.Configure<ShowCollection>(
    configuration.GetSection(nameof(ShowCollection)));

builder.Services.Configure<UserCollection>(
    configuration.GetSection(nameof(UserCollection)));

builder.Services.Configure<ShowsApiConfiguration>(
    configuration.GetSection(nameof(ShowsApiConfiguration)));

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton(typeof(MovieHubContext<>));
builder.Services.AddScoped<IShowRepository, ShowRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder => builder
.WithOrigins("http://localhost:3000", "https://localhost:3000")
.AllowAnyMethod()
.AllowAnyHeader()
.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

SeedDataFromExternalApi(app).GetAwaiter().GetResult();

app.Run();

async Task SeedDataFromExternalApi(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var repo = scope.ServiceProvider.GetRequiredService<IShowRepository>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        var apiConfig = scope.ServiceProvider.GetRequiredService<IOptions<ShowsApiConfiguration>>().Value;

        if (await repo.CollectionHasData()) return;

        var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
        var client = httpClientFactory.CreateClient("ExternalApi");

        HttpResponseMessage response = await client.GetAsync(apiConfig.Url);

        if (response.IsSuccessStatusCode)
        {
            try
            {
                string content = await response.Content.ReadAsStringAsync();
                var dataFromApi = JsonSerializer.Deserialize<List<Show>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    
                   
                });

                await repo.AddShows(dataFromApi);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
        else
        {
            logger.LogError("Error fetching data from api");
        }
    }
}
