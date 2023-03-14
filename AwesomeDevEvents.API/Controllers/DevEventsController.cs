using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Controllers
{
    /// <summary>
    /// ClasseApiController
    /// </summary>
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        private readonly ILogger<DevEventsController> _logger;
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public DevEventsController(ILogger<DevEventsController> logger, DevEventsDbContext context)
        {
            this._context = context;
            this._logger = logger;
        }

        /// <summary>
        /// Obter todos os eventos
        /// </summary>
        /// <returns>Coleção de eventos</returns>
        /// <response code="200">Sucesso</response>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {            
            _logger.LogInformation($"{nameof(GetAll)} na chamada!!");

            var devEvents = _context.DevEvents.Where(x => !x.IsDeleted).ToList();

            return Ok(devEvents);
        }

        /// <summary>
        /// Obter um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns>Dados do evento</returns>
        /// <response code="200">Sucesso</response>        
        /// <response code="404">Não encontrado</response> 
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            //traz os palestrantes quaando trouxer o evento
            var devEvent = _context.DevEvents
                .Include(de => de.Speakers)
                .SingleOrDefault(x => x.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        /// <summary>
        /// Cadastrar um Evento
        /// </summary>
        /// <param name="devEvent">Dados do evento</param>
        /// <remarks>Pode colocar qualquer informação aqui, por exemplo o json do objeto para criação</remarks>
        /// <returns>Objeto recem criado</returns>
        /// <response code="201">Sucesso</response>        
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);

            _context.SaveChanges(); //salva no banco de dados

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }

        /// <summary>
        /// Atualiza um Evento
        /// </summary>
        /// <remarks>remarks </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="input">Dados do evento</param>
        /// <returns>nada</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(Guid id, DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(x => x.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            _context.DevEvents.Update(devEvent);

            _context.SaveChanges(); //salva no banco de dados

            return NoContent();
        }

        /// <summary>
        /// Apaga lógicamente um evento
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns></returns>
        /// <returns>nada</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(x => x.Id == id);
            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();

            _context.SaveChanges(); //salva no banco de dados

            return NoContent();
        }

        /// <summary>
        /// Adiciona um Palestrante a um Evento
        /// </summary>
        /// <remarks>remarks</remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="speaker">Dados do Palestrante</param>
        /// <returns>nada</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpPost("{id}/speakers")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult PostSpeakres(Guid id, DevEventSpeaker speaker)
        {
            speaker.DevEventId = id;

            var devEvent = _context.DevEvents.Any(x => x.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }

            _context.DevEventSpeakers.Add(speaker);
            _context.SaveChanges(); //salva no banco de dados

            return NoContent();

        }
    }
}
