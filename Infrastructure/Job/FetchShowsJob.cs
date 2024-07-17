using Application.Interface;
using Domain.Entities;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
namespace Infrastructure.Job
{
    public class FetchShowsJob : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly HttpClient _httpClient;
        private static readonly ILog log = LogManager.GetLogger(typeof(FetchShowsJob));
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly int _intervalHours;

        /// <summary>
        /// Constructor del job para obtener y almacenar programas de TV desde una API externa.
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="scopeFactory"></param>
        public FetchShowsJob(HttpClient httpClient, IServiceScopeFactory scopeFactory, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _intervalHours = configuration.GetValue<int>("JobSettings:IntervalHours");
            _httpClient.BaseAddress = new Uri("https://api.tvmaze.com/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Inicia el job para ejecutarlo periódicamente según la configuración del temporizador.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelación.</param>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Configura el timer para ejecutar el job inmediatamente y luego cada x horas.
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(_intervalHours));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Método privado que se ejecuta cuando el temporizador dispara el evento.
        /// </summary>
        /// <param name="state">Estado del temporizador (no utilizado).</param>
        private async void DoWork(object state)
        {
            await ExecuteAsync();
        }

        /// <summary>
        /// Deserialize Shows
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private IEnumerable<Show> DeserializeShows(string json)
        {
            // Configurar settings para manejar valores nulos durante la deserialización
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore, // Ignorar valores nulos durante la deserialización
                Converters = { new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" } } // Opcional: Formato de fecha personalizado
            };

            // Deserializar como lista de JToken primero para manejar objetos y arrays correctamente
            var jArray = JArray.Parse(json);
            var shows = jArray.Select(jt =>
                {
                    try
                    {
                        var show = jt.ToObject<Show>(JsonSerializer.CreateDefault(settings));
                        return show;
                    }
                    catch (JsonReaderException ex)
                    {
                        Console.WriteLine($"Error deserializando show: {ex.Message}");
                        Console.WriteLine($"JSON devuelto: {json}");
                        return null;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error general deserializando show: {ex.Message}");
                        Console.WriteLine($"JSON devuelto: {json}");
                        return null;
                    }
                }).Where(s => s != null).ToList();

            return shows;

        }
        
        /// <summary>
        /// Una vez se obtiene el show deseralizado se llama al metodo que lo guarda en DB
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            try
            {
              
                var response = await _httpClient.GetAsync("shows");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var shows = DeserializeShows(json);

                    // Procesar los shows deserializados utilizando el servicio ShowService
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var showService = scope.ServiceProvider.GetRequiredService<IShowService>();                      
                        await showService.ProcessShowsAsync(shows);
                    }                   
                }
                else
                {
                    Console.WriteLine($"Failed to fetch shows. Status code: {response.StatusCode}");
                }
            }          
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Parar job cuando se detiene la aplicación
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Tarea asincrónica.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Liberar los recursos del timer cuando se detiene la aplicacion implícitamente 
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }

}
