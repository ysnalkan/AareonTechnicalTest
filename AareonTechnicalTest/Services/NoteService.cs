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
    public interface INoteService
    {
        Task<Note> GetById(int id);
        Task<IEnumerable<Note>> GetByTicketId(int id);
        Task AddNote(Note model);
        Task UpdateNote(int id, Note model);
        Task DeleteNote(int id);

    }

    public class NoteService : INoteService
    {

        private readonly ILogger<NoteService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public NoteService(
            ILogger<NoteService> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<Note> GetById(int id)
        {
            return await _unitOfWork.Notes.GetById(id);
        }

        public async Task<IEnumerable<Note>> GetByTicketId(int ticketId)
        {
            return await _unitOfWork.Notes.GetByTicketId(ticketId);
        }

        public async Task AddNote(Note model)
        {

            await _unitOfWork.Notes.Add(model);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteNote(int id)
        {
            await _unitOfWork.Notes.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateNote(int id,Note model)
        {
            await _unitOfWork.Notes.Update(id,model);
            await _unitOfWork.SaveAsync();
        }
    }
}
