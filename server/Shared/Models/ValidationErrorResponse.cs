using System.Text.Json.Serialization;

namespace Ecommerce.Shared.Models;

public class ValidationErrorResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IEnumerable<string>? Errors { get; set; }

    public string Message { get; set; } = "Validation Error";

    public ValidationErrorResponse(IEnumerable<string> errors)
    {
        Errors = errors;
    }
}