using System;

namespace Evento.Infrastructure.Dto
{
    public class AccountDto
    {
        public Guid Id{get;set;}
        public string Role{get;protected set;}
        public string Name{get;protected set;}
        public string Email{get;protected set;}
    }
}