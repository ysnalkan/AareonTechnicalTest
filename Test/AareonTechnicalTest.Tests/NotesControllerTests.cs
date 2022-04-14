using AareonTechnicalTest.Controllers;
using AareonTechnicalTest.DAL;
using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Post()
        {
            var data = new List<Note>
            {
                new Note { NoteText="first comment",PersonId=1,TicketId=1 }
            }.AsQueryable();

            var newNote = new NoteViewModel { NoteText = "test note", PersonId = 1, TicketId = 2 };

            var mockSet = new Mock<DbSet<Note>>();
            mockSet.As<IDbAsyncEnumerable<Note>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Note>(data.GetEnumerator()));

            mockSet.As<IQueryable<Note>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Note>(data.Provider));

            mockSet.As<IQueryable<Note>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Note>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Note>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>(new DbContextOptions<ApplicationContext>());
            mockContext.Setup(c => c.Notes).Returns(mockSet.Object);
            mockContext.Setup(x => x.Set<Note>()).Returns(mockSet.Object);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Note, Note>(data.First()))
                .Returns(data.First());

            mockMapper.Setup(x => x.Map<Note>(newNote))
                .Returns(new Note { NoteText = "test note", PersonId = 1, TicketId = 2 });

            var mockAccessor = new Mock<IHttpContextAccessor>();
            mockAccessor.Setup(x => x.HttpContext)
                .Returns(new DefaultHttpContext());

            var mock = new Mock<ILogger<NotesController>>();
            ILogger<NotesController> logger = mock.Object;

            var unitOfWork = new UnitOfWork(mockContext.Object, mockMapper.Object, mockAccessor.Object);

           var noteService = new NoteService(unitOfWork);

            var sut = new NotesController(noteService, logger, mockMapper.Object);

           //act
            var result = await sut.Post(newNote);
            var okResult = result as OkResult;

            // assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}