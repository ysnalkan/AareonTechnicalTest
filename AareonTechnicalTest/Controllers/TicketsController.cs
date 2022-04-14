using AareonTechnicalTest.Extensions;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AareonTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketsController> _logger;
        private readonly IMapper _mapper;

        public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger,
            IMapper mapper)
        {
            _ticketService=ticketService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<TicketsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TicketsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ticketEntity = await _ticketService.GetById(id);
            var ticketViewModel= ticketEntity.ToTicketViewModel(_mapper);
            return Ok(ticketViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Post(TicketViewModel  model)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.AddTicket(model.ToTicketEntity(_mapper));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.UpdateTicket(id, model.ToTicketEntity(_mapper));
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketService.DeleteTicket(id);
            return Ok();
        }
    }
}
