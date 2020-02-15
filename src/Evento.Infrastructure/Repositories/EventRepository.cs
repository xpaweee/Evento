using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Core.Domain;
using Evento.Core.Repositories;

namespace Evento.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private  static readonly ISet<Event> _events = new HashSet<Event>();

        
        public async Task<Event> GetAsync(Guid id)
            => await Task.FromResult(_events.SingleOrDefault(x => x.Id == id));

        public async Task<Event> GetAsync(string name)
            => await Task.FromResult(_events.SingleOrDefault(x=> x.Name.ToLowerInvariant() == name.ToLowerInvariant()));

        public async Task AddAsync(Event @event)
            => await Task.FromResult(_events.Add(@event));

        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            var events = _events.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()));
            }

            return await Task.FromResult(events);
        }
         

        public async Task DeleteAsync(Event @event)
            => await Task.FromResult(_events.Remove(@event));


        public async Task UpdateAsync(Event @event)
        {
            await Task.CompletedTask;
        }
    }
}