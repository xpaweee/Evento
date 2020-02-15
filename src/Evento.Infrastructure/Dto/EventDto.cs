using System;
using System.Collections.Generic;
using Evento.Core.Domain;

namespace Evento.Infrastructure.Dto
{
    public class EventDto
    {
        public Guid Id{get;protected set;}
        public string Name{get;protected set;}
        public string Description{get;protected set;}
        public DateTime StartDate{get;protected set;}
        public DateTime EndDate{get;protected set;}
        public DateTime UpdatedAt{get;protected set;}
        public int TicketsCount{get;protected set;}

    }
}