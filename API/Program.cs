using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application;
using MediatR;
using FluentValidation;
using Application.Behaviors;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(
      cfg => cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly)
);
builder.Services.AddSwaggerGen(c =>
{
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      c.RoutePrefix = string.Empty; // Sets Swagger UI at app root
});
app.MapControllers();

app.Run();
