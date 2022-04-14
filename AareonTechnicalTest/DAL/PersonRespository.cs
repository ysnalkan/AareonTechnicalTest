using AareonTechnicalTest.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AareonTechnicalTest.Extensions;
using Microsoft.AspNetCore.Http;

namespace AareonTechnicalTest.DAL
{
    public interface IPersonRepository : IGenericRepository<Person>
    {

    }

    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        private readonly IMapper _mapper;
        public PersonRepository(ApplicationContext context,IMapper mapper, IHttpContextAccessor _accessor) : base(context, _accessor) {
            _mapper = mapper;
        }

        public override async Task<IEnumerable<Person>> All()
        {
             return await dbSet.ToListAsync();
        }

        public override async Task<bool> Update(int id, Person entity)
        {
            var existingPerson = await dbSet.Where(x => x.Id == id)
                                                .FirstOrDefaultAsync();

            if (typeof(IAuditEntity).IsAssignableFrom(typeof(Person)))
            {
                var audit = (IAuditEntity)entity;
                audit.SetUpdater(CurrentUser, DateTime.UtcNow);
            }

            entity.ToPerson(existingPerson, _mapper);
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
