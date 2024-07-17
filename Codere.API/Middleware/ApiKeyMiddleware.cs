using log4net;

namespace Codere.API.Middleware
{
    /// <summary>
    /// Middleware para validar la API Key en las peticiones HTTP.
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string API_KEY_HEADER_NAME = "x-api-key";
        private readonly string _apiKey;
        private readonly ILog _logger;

        /// <summary>
        /// Constructor del middleware.
        /// </summary>
        /// <param name="next">Delegado para invocar el siguiente middleware.</param>
        /// <param name="configuration">Configuración de la aplicación para obtener la API Key.</param>
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _apiKey = configuration.GetValue<string>("ApiKey");
            _logger = LogManager.GetLogger(typeof(ApiKeyMiddleware));
        }

        /// <summary>
        /// Método para procesar las peticiones HTTP y validar la API Key.
        /// </summary>
        /// <param name="context">Contexto HTTP actual.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Verificar si el header contiene la API Key
                if (!context.Request.Headers.TryGetValue(API_KEY_HEADER_NAME, out var extractedApiKey))
                {
                    context.Response.StatusCode = 401; // Unauthorized
                    await context.Response.WriteAsync("API Key is missing");
                    return;
                }

                // Comparar la API Key extraída con la API Key configurada
                if (!_apiKey.Equals(extractedApiKey))
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync("Unauthorized client");
                    return;
                }

                // Si la API Key es válida, continuar con el siguiente middleware
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.Error($"Error en el middleware ApiKeyMiddleware: {ex.Message}", ex);
                context.Response.StatusCode = 500; // Internal Server Error
                await context.Response.WriteAsync("Internal server error");
            }
        }
    }
}
