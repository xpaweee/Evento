using System;
using Evento.Infrastructure.Dto;

namespace Evento.Infrastructure.Services
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(Guid userId, string role);
    }
}