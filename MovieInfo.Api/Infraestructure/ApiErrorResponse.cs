using FluentResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MovieInfo.Api.Infraestructure;

public class ApiErrorResponse
{
    public string Type { get; set; }          // Type ej: NotFound, ValidationError, FileSaveError, etc.
    public IEnumerable<string> Errors { get; set; }  // Messages

    public ApiErrorResponse(string type, IEnumerable<string> errors)
    {
        Type = type;
        Errors = errors;
    }
    public ApiErrorResponse(string type, string error)
    {
        Type = type;
        Errors = new[] { error };
    }

    public ApiErrorResponse(string type, IList<IError> errors)
    {
        Type = type;
        Errors = errors.Select(error => error.Message);
    }
}