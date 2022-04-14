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
    public interface ITicketService
    {
        Task<Ticket> GetById(int id);
        Task<IEnumerable<Ticket>> GetAll();
        Task AddTicket(Ticket model);
        Task UpdateTicket(int id,Ticket model);
        Task DeleteTicket(int id);

    }

    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ticket> GetById(int id)
        {
            return await _unitOfWork.Tickets.GetById(id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _unitOfWork.Tickets.All();
        }

        public async Task AddTicket(Ticket model)
        {

            await _unitOfWork.Tickets.Add(model);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteTicket(int id)
        {
            await _unitOfWork.Tickets.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateTicket(int id,Ticket model)
        {
            await _unitOfWork.Tickets.Update(id,model);
            await _unitOfWork.SaveAsync();
        }
    }
}
