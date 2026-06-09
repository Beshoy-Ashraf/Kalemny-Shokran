using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application;
using MediatR;
using FluentValidation;
using Application.Behaviors;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(
      cfg => cfg.RegisterServicesFromAssembly(typeof(IAssemblyMarker).Assembly)
);

builder.Services.AddValidatorsFromAssembly(typeof(IAssemblyMarker).Assembly);

builder.Services.AddDbContext<AppDBContext>(
options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
