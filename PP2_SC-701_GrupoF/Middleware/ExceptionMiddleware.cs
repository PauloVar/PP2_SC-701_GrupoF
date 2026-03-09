using System.Net;

namespace PP2_SC_701_GrupoF.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html; charset=utf-8";

                var html = $@"
                    <html>
                    <head>
                        <title>Error</title>
                        <style>
                            body {{
                                font-family: Arial, sans-serif;
                                margin: 40px;
                                background-color: #f8f9fa;
                            }}
                            .error-box {{
                                background: white;
                                border: 1px solid #ddd;
                                padding: 20px;
                                border-radius: 8px;
                                box-shadow: 0 2px 6px rgba(0,0,0,0.1);
                            }}
                            h1 {{
                                color: #dc3545;
                            }}
                            p {{
                                color: #333;
                            }}
                            a {{
                                display: inline-block;
                                margin-top: 15px;
                                text-decoration: none;
                                color: white;
                                background: #0d6efd;
                                padding: 10px 16px;
                                border-radius: 6px;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='error-box'>
                            <h1>Ocurrió un error en el sistema</h1>
                            <p>{ex.Message}</p>
                            <a href='/Home/Index'>Volver al inicio</a>
                        </div>
                    </body>
                    </html>";

                await context.Response.WriteAsync(html);
            }
        }
    }
}