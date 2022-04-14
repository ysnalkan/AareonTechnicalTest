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
    public interface INoteRepository : IGenericRepository<Note>
    {
        Task<IEnumerable<Note>> GetByTicketId(int id);
    }

    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {

        private readonly IMapper _mapper;

        public NoteRepository(ApplicationContext context, IMapper mapper, IHttpContextAccessor _accessor) : base(context, _accessor)
        {
            _mapper = mapper;
        }

        public  async Task<IEnumerable<Note>> GetByTicketId(int ticketId)
        {
             return await dbSet.Where(x=>x.Ticket.Id==ticketId).ToListAsync();
        }

        public override async Task<IEnumerable<Note>> All()
        {

             return await dbSet.ToListAsync();
        }

        public override async Task<bool> Update(int id, Note entity)
        {

            var existingNote= await dbSet.Where(x => x.Id == id)
                                                .FirstOrDefaultAsync();

            if (typeof(IAuditEntity).IsAssignableFrom(typeof(Note)))
            {
                var audit = (IAuditEntity)entity;
                audit.SetUpdater(CurrentUser, DateTime.UtcNow);
            }

            entity.ToNote(existingNote, _mapper);
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
