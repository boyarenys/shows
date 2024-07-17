using Application.Interface;
using Domain.Entities;
using Infrastructure.Job;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Codere.API.Controllers
{
    [Route("shows")] 
    [ApiController]
    public class ShowController : Controller
    {
        private readonly FetchShowsJob _fetchShowsJob;       
        private readonly IShowService _showService;
        private readonly ILog log = LogManager.GetLogger(typeof(ShowController));


        public ShowController(FetchShowsJob fetchShowsJob, IShowService showService)
        {
            _fetchShowsJob = fetchShowsJob;
            _showService = showService;
        }


        /// <summary>
        /// Obtiene y almacena shows desde una API externa.
        /// </summary>
        /// <returns></returns>
        [HttpPost("CallJob")]
        public async Task<IActionResult> FetchShows()
        {
            log.Info("[FetchShows]: Inicio");
            try
            {
                await _fetchShowsJob.ExecuteAsync();
                log.Info("[FetchShows] ejecutado con éxito");
                return Ok("Shows obtenidos y almacenados exitosamente.");
            }
            catch (HttpRequestException ex)
            {
                log.Error("[FetchShows] : Error HTTP al obtener shows", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al obtener shows"); 
            }
            catch (JsonReaderException ex)
           {
                log.Error("[FetchShows] : Error de deserialización JSON", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno al procesar los datos de shows"); 
            }
            catch (Exception ex)
            {
                log.Error("[FetchShows] : Error ejecutando job FetchShowsJob", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error ejecutando job FetchShowsJob: {ex.Message}");
            }
        }

        /// <summary>
        /// Recupera todos los shows de la base de datos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllShows()
        {
            log.Info("[GetAllShows] : Inicio");
            try
            {
                var shows = await _showService.GetAllAsync();

                if (shows == null || shows.Count == 0)
                {
                    log.Warn("No se encontraron shows.");
                    return NotFound("No se encontraron shows.");
                }

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(shows);
                return Content(json, "application/json");

            }
            catch (Exception ex)
            {
                log.Error("[GetAllShows] : Error al obtener todos los shows", ex);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// Recupera un show específico por ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: /shows/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetShowById(int id)
        {
            log.Info($"[GetShowById] : Inicio con ID: {id}");
            try
            {
                var show = await _showService.GetByIdAsync(id);
                if (show == null)
                {
                    log.Warn($"Show con ID: {id} no encontrado");
                    return NotFound();
                }
                log.Info($"[GetShowById] con ID: {id} ejecutado con éxito");
                return Content(show.ToString(), "application/json");
            }
            catch (Exception ex)
            {
                log.Error($"[GetShowById] : Error al obtener el show con ID {id}", ex);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
   
    
    

