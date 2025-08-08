namespace logifly.api.Middlewares
{
    public class ResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseLoggingMiddleware> _logger;

        public ResponseLoggingMiddleware(RequestDelegate next, ILogger<ResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Orijinal response body stream'i sakla
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); // request pipeline devam etsin

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            if (context.Response.StatusCode >= 400)
            {
                // Elasticsearch'e structured log olarak gönder
                _logger.LogWarning("API Error Response: {@StatusCode} {@Body}", context.Response.StatusCode, bodyText);
            }

            // Body'yi orijinal stream'e kopyala
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
    }
