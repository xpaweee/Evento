using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController  :  ApiControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMemoryCache _memoryCache;
        public EventsController(IEventService eventService, IMemoryCache memoryCache)
        {
            _eventService = eventService;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            //throw new ArgumentException("tst");
            var events = _memoryCache.Get<IEnumerable<EventDto>>("events");
            if(events == null)
            {
                 events = await _eventService.GetBrowse(name);
                 _memoryCache.Set("events",events,TimeSpan.FromMinutes(1));

            }

            return Json(events);
        }
        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var events = await _eventService.GetAsync(eventId);
            if(@events == null)
                return NotFound();

            return Json(events);
        }
        [HttpPost]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Post([FromBody]CreateEvent command)
        {
            command.EventId =Guid.NewGuid();
            await _eventService.CreateAsync(command.EventId,command.Name,command.Description,command.StartDate,command.EndDate);
            await _eventService.AddTicketsAsync(command.EventId,command.Tickets,command.Price);

            return Created($"events/{command.EventId}",null);
        }

        [HttpPut("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
          public async Task<IActionResult> Put(Guid eventId,[FromBody]UpdateEvent command)
        {
            await _eventService.UpdateAsync(command.EventId,command.Name,command.Description);

            //204
            return NoContent();
        }

         [HttpDelete("{eventId}")]
         [Authorize(Policy = "HasAdminRole")]
          public async Task<IActionResult> Delete(Guid eventId)
        {
            await _eventService.DeleteAsync(eventId);

            //204
            return NoContent();
        }

    }
}