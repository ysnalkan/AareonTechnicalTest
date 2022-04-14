using AareonTechnicalTest.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.Mappings
{
    public class MappingPofile : Profile
    {
        public MappingPofile()
        {
            CreateMap<Ticket, Ticket>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore());
            CreateMap<TicketViewModel, Ticket>();
            CreateMap<Ticket, TicketViewModel>();

            CreateMap<Person, Person>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore());
            CreateMap<PersonViewModel, Person>();
            CreateMap<Person, PersonViewModel>();

            CreateMap<Note, Note>()
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedDate, opt => opt.Ignore());
            CreateMap<NoteViewModel, Note>();
            CreateMap<Note, NoteViewModel>();

        }

    }
}
