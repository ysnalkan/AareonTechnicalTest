using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.DAL
{

    public interface IUnitOfWork
    {
        IPersonRepository People { get; }
        ITicketRepository Tickets { get; }
        INoteRepository Notes { get; }

        Task SaveAsync();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        protected IHttpContextAccessor _accessor;

        public IPersonRepository People { get; private set; }
        public ITicketRepository Tickets { get; private set; }
        public INoteRepository Notes { get; private set; }

        public UnitOfWork(ApplicationContext context, IMapper mapper, IHttpContextAccessor accessor)
        {
            _context = context;
            _mapper = mapper;
            _accessor = accessor;

            People = new PersonRepository(context, _mapper, _accessor);
            Tickets = new TicketRepository(context, _mapper, _accessor);
            Notes = new NoteRepository(context, _mapper, _accessor);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync(_accessor.HttpContext.User.Identity.Name);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
