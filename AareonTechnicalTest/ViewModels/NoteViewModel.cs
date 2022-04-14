using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class NoteViewModel
    {
        public int Id { get; }

        public string NoteText { get; set; }

        public int TicketId { get; set; }

        public int PersonId { get; set; }
    }
}
