using System.Diagnostics;
using Application.Common;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EShopBackendApi.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult(this Error error, ControllerBase controller)
    {
        return error.ErrorType switch
        {
            ErrorType.Unauthorized => controller.Unauthorized(error),
            ErrorType.NotFound => controller.NotFound(error),
            ErrorType.Conflict => controller.Conflict(error),
            ErrorType.Validation => controller.BadRequest(error),
            ErrorType.Forbidden => controller.Forbid(),
            _ => controller.StatusCode(StatusCodes.Status500InternalServerError, error)
        };
    }
}