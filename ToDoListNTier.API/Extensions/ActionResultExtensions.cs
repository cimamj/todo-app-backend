using Microsoft.AspNetCore.Mvc;
using ToDoListNTier.BLL.Results;

namespace ToDoListNTier.API.Extensions
{
    public static class ActionResultExtensions
    {
        const int NotFoundCode = 404;
        const int BadRequestCode = 400;

        private static object WrapErrors(string? error) => new { errors = error };

        public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller, Func<T, IActionResult>? onSuccess = null)
        {
            if (!result.IsSuccess)
            {
                if (result.StatusCode == NotFoundCode)
                    return controller.NotFound(WrapErrors(result.Error));

                if (result.StatusCode == BadRequestCode)
                    return controller.BadRequest(WrapErrors(result.Error));

                return controller.BadRequest(WrapErrors(result.Error));
            }

            if (onSuccess != null)
            {
                return onSuccess(result.Value!);
            }

            return controller.Ok(result.Value);
        }

        public static IActionResult ToActionResult(this Result result, ControllerBase controller)
        {
            if (!result.IsSuccess)
            {
                if (result.StatusCode == NotFoundCode)
                    return controller.NotFound(WrapErrors(result.Error));

                if (result.StatusCode == BadRequestCode)
                    return controller.BadRequest(WrapErrors(result.Error));

                return controller.BadRequest(WrapErrors(result.Error));
            }
            return controller.NoContent();
        }
    }
}
