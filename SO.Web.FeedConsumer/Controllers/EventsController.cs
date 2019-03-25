using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SO.Data;
using SO.Data.Entities;
using SO.Server.FeedConsumer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SO.Web.FeedConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public EventsController(IUnitOfWork uow, IMapper mapper)
        {
            _eventRepository = uow.GetRepository<Event>();
            _mapper = mapper;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IEnumerable<EventModel>> Get()
        {
            return _mapper.Map<IEnumerable<EventModel>>(await _eventRepository.GetAll(include: x => x.Include(ev => ev.Matches)
                                     .ThenInclude(ma => ma.Bets)
                                     .ThenInclude(bet => bet.Odds))
                                     .ToListAsync());
        }
    }
}
