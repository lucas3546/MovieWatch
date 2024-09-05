using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MovieInfo.Api.Extensions;

public static class ModelStateExtensions
{
    public static string GetAllErrors(this ModelStateDictionary modelStateDictionary)
    {
        return string.Join("; ", modelStateDictionary.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage));
    }
}
