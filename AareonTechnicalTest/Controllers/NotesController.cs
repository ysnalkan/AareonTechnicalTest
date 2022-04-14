using AareonTechnicalTest.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AareonTechnicalTest.Extensions;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Filters;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ILogger<NotesController> _logger;
        private readonly IMapper _mapper;

        public NotesController(INoteService noteService, ILogger<NotesController> logger,
            IMapper mapper)
        {
            _noteService = noteService;
            _logger = logger;
            _mapper = mapper;
        }

        

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var noteEntity = await _noteService.GetById(id);
            var noteViewModel = noteEntity.ToNoteViewModel(_mapper);
            return Ok(noteViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Post(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _noteService.AddNote(model.ToNoteEntity(_mapper));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _noteService.UpdateNote(id, model.ToNoteEntity(_mapper));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete()]
        [Route("{id}/Persons/{personId}")]
        [Authorize(PermissionItem.Note, PermissionAction.Delete)]
        public async Task<IActionResult> Delete(int personId,int id)
        {
            await _noteService.DeleteNote(id);
            return Ok();
        }
    }


}
