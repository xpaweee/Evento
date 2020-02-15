using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Core.Domain;

namespace Evento.Infrastructure.Services
{
    public interface IEventService
    {
         Task<Event> GetAsync(Guid id);
         Task<Event> GetAsync(string name);
         Task<IEnumerable<Event>> GetBrowse(string name = null);
         Task CreateAsync(Guid id,string name, string description, DateTime startDate,DateTime endDate);
         Task UpdateAsync(Guid id,string name, string description);
         Task DeleteAsync(Guid id);
         
    }
}