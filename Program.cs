using mero_movie_api.Data;
using mero_movie_api.Repositories;
using mero_movie_api.Repository.Interface;
using mero_movie_api.Services;
using mero_movie_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddScoped<IMovieService, MovieService>();
// builder.Services.AddScoped<IMovieRepository, MovieRepository>();
// builder.Services.AddHttpClient();

builder.Services.AddControllers();

// Register services
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IWatchListService, WatchListService>();

// Register repository with HttpClient
builder.Services.AddHttpClient<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IWatchListRepository, WatchListRepository>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    options.UseMySQL(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();