using System;

namespace Evento.Infrastructure.Dto
{
    public class TicketDetailsDto :TicketDto
    {
        public Guid EventId{get;set;}
        public string EventName{get;set;}
    }
}