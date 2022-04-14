using AareonTechnicalTest.Extensions;
using AareonTechnicalTest.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.DAL
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {

    }

    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private ILogger _logger;
        private readonly IMapper _mapper;

        public TicketRepository(ApplicationContext context, ILogger logger, IMapper mapper, IHttpContextAccessor _accessor) : base(context, _accessor, logger)
        {
            _mapper = mapper;
        }

        public override async Task<IEnumerable<Ticket>> All()
        {
              return await dbSet.ToListAsync();
        }

        public override async Task<bool> Update(int id,Ticket entity)
        {
            var existingTicket = await dbSet.Where(x => x.Id == id)
                                                .FirstOrDefaultAsync();

            if (typeof(IAuditEntity).IsAssignableFrom(typeof(Ticket)))
            {
                var audit = (IAuditEntity)entity;
                audit.SetUpdater(CurrentUser, DateTime.UtcNow);
            }

            entity.ToTicket(existingTicket, _mapper);

            return true;
        }

        public override async Task<bool> Delete(int id)
        {
            var exist = await dbSet.Where(x => x.Id == id)
                                    .FirstOrDefaultAsync();

            if (exist == null) return false;

            dbSet.Remove(exist);

            return true;
        }
    }
}
