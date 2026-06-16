using Application.Common.Exceptions;
using Application.Common.NotFoundException;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.Exceptions;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
      public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
      {
            ProblemDetails problemDetails = exception switch
            {
                  ValidationException ex => new ValidationProblemDetails
                  (
                        ex.Errors.GroupBy(g => g.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray())

                  )
                  {
                        Title = "Validation Failed!",
                        Status = StatusCodes.Status400BadRequest
                  },
                  UnauthorizedException ex => new ProblemDetails
                  {
                        Title = "Email Not Unauthorized!",
                        Detail = ex.Message,
                        Status = StatusCodes.Status404NotFound
                  },
                  NotFoundException ex => new ProblemDetails
                  {
                        Title = "Resource Not Found!",
                        Detail = ex.Message,
                        Status = StatusCodes.Status404NotFound
                  },
                  _ => new ProblemDetails
                  {
                        Title = "Resource Not Found!",
                        Detail = "UnHandle Exception!",
                        Status = StatusCodes.Status500InternalServerError
                  },
            };
            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            await problemDetailsService.WriteAsync(new ProblemDetailsContext
            {
                  HttpContext = httpContext,
                  ProblemDetails = problemDetails,
            });
            return true;
      }
}
