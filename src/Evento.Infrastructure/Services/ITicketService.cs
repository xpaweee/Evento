using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Evento.Infrastructure.Dto;

namespace Evento.Infrastructure.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetForUserAsync(Guid userId);
         Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId);
         Task PurchaseAsync(Guid userId,Guid eventId, int amount);
         Task CancelAsync(Guid userId,Guid eventId, int amount);
    }
}