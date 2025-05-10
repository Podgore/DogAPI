using DogAPI.DAL.DBContext;
using DogAPI.DAL.Repository.Interface;
using DogAPI.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using DogAPI.BLL.Services;
using DogAPI.BLL.Profiles;
using DogAPI.Extensions;
using DogAPI.Validator;
using FluentValidation.AspNetCore;
using FluentValidation;
using DogAPI.BLL.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Mapper
builder.Services.AddAutoMapper(typeof(DogProfile));

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository
builder.Services.AddScoped<IDogRepository, DogRepository>();
builder.Services.AddScoped<IAnimalShelterRepository, AnimalShelterRepository>();

builder.Services.AddScoped<IDogService, DogService>();
builder.Services.AddScoped<IAnimalShelterService, AnimalShelterService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Validators
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<GetDogsRequestValidator>();

builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

app.UseRateLimiting();

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
