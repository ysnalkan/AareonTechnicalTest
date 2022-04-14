using AareonTechnicalTest.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Extensions
{
    public static class EntityModelBuilder
    {
        public static TicketViewModel ToTicketViewModel(this Ticket ticket, IMapper mapper)
        {
            return mapper.Map<TicketViewModel>(ticket);
        }

        public static Ticket ToTicketEntity(this TicketViewModel ticket, IMapper mapper)
        {
            return mapper.Map<Ticket>(ticket);
        }

        public static Ticket ToTicket(this Ticket ticket,Ticket destionationTicket, IMapper mapper)
        {
            return mapper.Map(ticket, destionationTicket);
        }

        public static NoteViewModel ToNoteViewModel(this Note note, IMapper mapper)
        {
            return mapper.Map<NoteViewModel>(note);
        }

        public static Note ToNoteEntity(this NoteViewModel note, IMapper mapper)
        {
            return mapper.Map<Note>(note);
        }

        public static Note ToNote(this Note note, Note destionationNote, IMapper mapper)
        {
            return mapper.Map(note, destionationNote);
        }

        public static PersonViewModel ToPersonViewModel(this Person Person, IMapper mapper)
        {
            return mapper.Map<PersonViewModel>(Person);
        }

        public static Person ToPersonEntity(this PersonViewModel Person, IMapper mapper)
        {
            return mapper.Map<Person>(Person);
        }

        public static Person ToPerson(this Person Person, Person destionationPerson, IMapper mapper)
        {
            return mapper.Map(Person, destionationPerson);
        }

    }
}
