using System.ComponentModel.DataAnnotations;

namespace AareonTechnicalTest.Models
{
    public class TicketViewModel
    {
        public int Id { get; }
        public string Content { get; set; }
        public int PersonId { get; set; }
    }
}
