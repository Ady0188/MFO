using MFO.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MFO.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> FromCommandResult<T>(CommandResult<T> result, string? createdAtAction = null, object? routeValues = null)
    {
        if (result.Error == CommandError.NotFound)
        {
            return NotFound();
        }

        if (result.Error == CommandError.Conflict)
        {
            return Conflict();
        }

        if (result.Error == CommandError.MissingReferences)
        {
            return ValidationProblem(ToModelState(result.MissingReferences));
        }

        if (result.Value is null)
        {
            return BadRequest();
        }

        return createdAtAction is null
            ? Ok(result.Value)
            : CreatedAtAction(createdAtAction, routeValues, result.Value);
    }

    private static ModelStateDictionary ToModelState(IReadOnlyList<string> missing)
    {
        var modelState = new ModelStateDictionary();
        foreach (var item in missing)
        {
            modelState.AddModelError(item, "Missing reference.");
        }

        return modelState;
    }
}
