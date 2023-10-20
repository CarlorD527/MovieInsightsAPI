using Application.Interfaces;
using Application.Services;
using Infrastructure.MongoConfig;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<MovieDatabaseSettings>(
    builder.Configuration.GetSection("MoviesDatabase")
    );
builder.Services.AddSingleton<MovieCollection>();
builder.Services.AddSingleton<UserCollection>();
builder.Services.AddSingleton<ReviewCollection>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Application.Validators.MovieValidators>();
builder.Services.AddScoped<Application.Validators.UserValidators>();
builder.Services.AddScoped<Application.Validators.ReviewValidators>();


builder.Services.AddScoped<IMovieApplication, MovieApplication>();
builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped<IReviewApplication, ReviewApplication>();

//Cors
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));



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
