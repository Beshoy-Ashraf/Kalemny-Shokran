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
using API.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Authentication;

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
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidateBehavior<,>));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
          options.TokenValidationParameters = new TokenValidationParameters
          {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "KalemnyShokranApi",
                ValidAudience = "KalemnyShokranClient",
                IssuerSigningKey = new SymmetricSecurityKey(
                  Encoding.UTF8.GetBytes("YourSuperSecretKeyThatIsLongEnoughToSecureTheApi123!"))
          };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddProblemDetails();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
      c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      c.RoutePrefix = string.Empty;
});
app.MapControllers();

app.UseExceptionHandler();
app.Run();
