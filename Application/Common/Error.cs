namespace Application.Common;

public sealed record Error(ErrorType ErrorType ,string Code, string? Description = null)
{
    public static readonly Error None = new Error(ErrorType.None,string.Empty);
    
    public static Error NotFound(string entity, string message) =>
        new(ErrorType.NotFound, $"{entity}.NotFound", message);

    public static Error Validation(string field, string message) =>
        new(ErrorType.Validation, $"Validation.{field}", message);
    
    public static Error Conflict(string field, string description) =>
        new(ErrorType.Conflict, $"{field}.Conflict", description);

    public static Error NotAuthorized(string code, string description) =>
        new(ErrorType.Unauthorized, code, description);
}