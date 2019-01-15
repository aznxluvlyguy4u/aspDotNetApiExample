using System;
using System.Net;

/// <inheritdoc/>
public sealed class ProducesResponseAttribute : Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute
{
    public ProducesResponseAttribute(int statusCode) : base(statusCode)
    {
    }

    public ProducesResponseAttribute(Type type, HttpStatusCode statusCode) : base(type, (int)statusCode)
    {
    }

    public ProducesResponseAttribute(Type type, int statusCode) : base(type, statusCode)
    {
    }
}