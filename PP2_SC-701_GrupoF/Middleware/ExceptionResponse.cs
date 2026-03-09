using System.Net;

namespace PP2_SC_701_GrupoF.Middleware
{
    public record ExceptionResponse(HttpStatusCode statusCode, string description);
}