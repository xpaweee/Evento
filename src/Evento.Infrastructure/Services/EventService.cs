using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using NLog;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public EventService(IEventRepository eventRepositry, IMapper mapper)
        {
            _eventRepository = eventRepositry;
            _mapper = mapper;
        }
          public async Task<EventDetailsDto> GetAsync(Guid id)
          {
              var @event = await _eventRepository.GetAsync(id);

            return _mapper.Map<EventDetailsDto>(@event);
            //   return new EventDto
            //   {
            //      Id = @event.Id,
            //      Name = @event.Name
            //   };
          }
            

        public async Task<EventDetailsDto> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);

            return _mapper.Map<EventDetailsDto>(@event);
            //   return new EventDto
            //   {
            //      Id = @event.Id,
            //      Name = @event.Name
            //   };
        }
        
        public async Task<IEnumerable<EventDto>> GetBrowse(string name = null)
        {
            Logger.Info("Fetching events");
            var events = await _eventRepository.BrowseAsync(name);

            return _mapper.Map<IEnumerable<EventDto>>(events);
            // return events.Select( x => new EventDto
            // {
            //     Id = x.Id,
            //     Name = x.Name
            // });

              
        }
        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(name);
            if(@event != null)
                throw new Exception($"Event named: '{name}' already exists");

            @event = new Event(id,name,description,startDate,endDate);
            await _eventRepository.AddAsync(@event);
        }      
        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var @event = await _eventRepository.GetAsync(name);
            if(@event != null)
                throw new Exception($"Event named: '{name}' already exists");
            @event = await _eventRepository.GetOrFailAsync(id);
            @event.SetName(name);            
            @event.SetDescription(description);            
            await _eventRepository.UpdateAsync(@event);
        }

        public async Task DeleteAsync(Guid id)
        {
           var @event = await _eventRepository.GetOrFailAsync(id);
           await _eventRepository.DeleteAsync(@event);
        }

        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            var @event = await _eventRepository.GetOrFailAsync(eventId);

            @event.AddTickets(amount,price);
            await _eventRepository.UpdateAsync(@event);
        }
    }
}