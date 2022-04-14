using AareonTechnicalTest.DAL;
using AareonTechnicalTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Services
{
    public interface IPersonService
    {
        Task<Person> GetById(int id);
        Task AddPerson(Person model);
        Task UpdatePerson(Person model);
        Task DeletePerson(int personId);
        Task<bool> CanDeleteNote(int personId);
    }

    public class PersonService : IPersonService
    {

        private readonly IUnitOfWork _unitOfWork;

        public PersonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> GetById(int id)
        {
            return await _unitOfWork.People.GetById(id);
        }

        public async Task<bool> CanDeleteNote(int personId)
        {
            var person=await _unitOfWork.People.GetById(personId);
            return person?.IsAdmin ?? false;
        }

        public async Task AddPerson(Person model)
        {

            await _unitOfWork.People.Add(model);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeletePerson(int personId)
        {
            await _unitOfWork.People.Delete(personId);
            await _unitOfWork.SaveAsync();
        }

        public Task UpdatePerson(Person model)
        {
            throw new NotImplementedException();
        }
    }
}
