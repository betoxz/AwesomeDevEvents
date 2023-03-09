using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {
        private readonly DevEventsDbContext _context;
        public DevEventsController(DevEventsDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(x => !x.IsDeleted).ToList();

            return Ok(devEvents);
        }

        [HttpGet("{id}")]
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

        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);

            _context.SaveChanges(); //salva no banco de dados

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeakres(Guid id, DevEventSpeaker speaker)
        {
            speaker.DevEventId = id;

            var devEvent = _context.DevEvents.Any(x => x.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            _context.DevEventSpeakers.Add(speaker);
            _context.SaveChanges(); //salva no banco de dados

            return NoContent();

        }
    }
}
