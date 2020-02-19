using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog;

namespace Evento.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private static readonly Logger Logger  =  LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        public DataInitializer(IUserService userService, IEventService eventService)
        {
            _userService = userService;
            _eventService = eventService;
        }
        public async Task SeedAsync()
        {
            Logger.Info("Initializing data...");
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(),"user@email.com","default user","secret"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(),"admin@email.com","default user","secret","admin"));
            Logger.Info("Created users.");
            for (int i = 0; i < 10; i++)
            {
                var eventId = Guid.NewGuid();
                var name = $"Event {i}";
                var description =  $"{name} desc.";
                var startDate =DateTime.UtcNow.AddHours(3);
                var endDate =startDate.AddHours(2);
                tasks.Add(_eventService.CreateAsync(eventId,name,description,startDate,endDate));
                tasks.Add(_eventService.AddTicketsAsync(eventId,1000,100));
                Logger.Info($"Created event - {name}");
            }
            await Task.WhenAll(tasks);
            

        }
    }
}