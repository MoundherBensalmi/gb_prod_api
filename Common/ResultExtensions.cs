using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace gb_prod_api.Common
{
    public static class ResultExtensions
    {
        // public static ActionResult<T> ToActionResult<T>(this Result<T> result, ControllerBase controller)
        // {
        //     if (result.IsSuccess)
        //         return controller.Ok(result.Data);

        //     var error = result.Error!;
        //     var problem = new ProblemDetails
        //     {
        //         Title = error.Type.ToString(),
        //         Detail = error.Message,
        //         Status = error.Type switch
        //         {
        //             AppErrorType.Validation => StatusCodes.Status400BadRequest,
        //             AppErrorType.NotFound => StatusCodes.Status404NotFound,
        //             AppErrorType.Conflict => StatusCodes.Status409Conflict,
        //             _ => StatusCodes.Status500InternalServerError
        //         }
        //     };

        //     if (error.Field is not null)
        //         problem.Extensions["field"] = error.Field;

        //     return controller.StatusCode(problem.Status!.Value, problem);
        // }

        // // For 201 Created responses
        // public static ActionResult<T> ToCreatedResult<T>(this Result<T> result, ControllerBase controller, string actionName, Func<T, object> routeValues)
        // {
        //     if (!result.IsSuccess)
        //         return result.ToActionResult(controller);

        //     return controller.CreatedAtAction(actionName, routeValues(result.Data!), result.Data);
        // }

        public static ActionResult ToErrorActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccess)
                throw new InvalidOperationException("ToErrorActionResult called on a successful result.");

            var error = result.Error!;
            var problem = new ProblemDetails
            {
                Title = error.Type.ToString(),
                Detail = error.Message,
                Status = error.Type switch
                {
                    AppErrorType.Validation => StatusCodes.Status400BadRequest,
                    AppErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                    AppErrorType.NotFound => StatusCodes.Status404NotFound,
                    AppErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                }
            };

            if (error.Field is not null)
                problem.Extensions["field"] = error.Field;

            return controller.StatusCode(problem.Status!.Value, problem);
        }
    }
}