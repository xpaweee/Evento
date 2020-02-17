namespace Evento.Infrastructure.Dto
{
    public class TokenDto
    {
        public string Token{get;set;}
        public string Role{get;set;}
        public long Expire{get;set;}
    }
}