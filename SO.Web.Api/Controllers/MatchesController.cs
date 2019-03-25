using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SO.Data;
using SO.Data.Entities;
using SO.Data.Repositories;
using SO.Server.FeedConsumer.Models;
using SO.Web.Api.DataFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SO.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IRepository<Match> _matchRepository;
        private readonly IMapper _mapper;

        public MatchesController(IUnitOfWork uow, IMapper mapper)
        {
            _matchRepository = uow.GetRepository<Match>();
            _mapper = mapper;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<IEnumerable<MatchModel>> Get()
        {
            var result = await _matchRepository.GetAll(
                predicate: MatchesForNext24hoursPredicate.Predicate,
                include: x => x.Include(ma => ma.Bets)
                        .ThenInclude(bet => bet.Odds))
                        .ToListAsync();

            return _mapper.Map<IEnumerable<MatchModel>>(result);
        }

       
    }
}
