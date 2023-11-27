using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TSCore.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    private const string NotFound = "not found.";

    public NotFoundException(string message) : base(message)
    {
    }

    public static void ThrowIfNull<T>(T entity, object param = null, string message = NotFound)
    {
        ThrowIfNullAs<T>(entity, param, message);
    }

    public static void ThrowIfNullAs<T>(object entity, object param = null, string message = NotFound)
    {
        if (entity is null)
        {
            var entryName = typeof(T).Name;
            var msg = string.Join(" ", entryName, param, message ?? NotFound);
            throw new NotFoundException(msg);
        }
    }
}