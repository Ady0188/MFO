namespace MFO.Application.Common.Models;

public enum CommandError
{
    None = 0,
    NotFound = 1,
    Conflict = 2,
    MissingReferences = 3
}

public sealed record CommandResult<T>(T? Value, CommandError Error, IReadOnlyList<string> MissingReferences)
{
    public bool IsSuccess => Error == CommandError.None;

    public static CommandResult<T> Success(T value) =>
        new(value, CommandError.None, Array.Empty<string>());

    public static CommandResult<T> NotFound() =>
        new(default, CommandError.NotFound, Array.Empty<string>());

    public static CommandResult<T> Conflict() =>
        new(default, CommandError.Conflict, Array.Empty<string>());

    public static CommandResult<T> Missing(IReadOnlyList<string> missing) =>
        new(default, CommandError.MissingReferences, missing);
}
