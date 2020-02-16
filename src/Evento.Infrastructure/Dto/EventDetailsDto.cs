using System.Collections.Generic;

namespace Evento.Infrastructure.Dto
{
    public class EventDetailsDto : EventDto     
    {
        public IEnumerable<TicketDto> Tickets {get;set;}
    }
}