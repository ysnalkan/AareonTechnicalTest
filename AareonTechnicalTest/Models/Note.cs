using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AareonTechnicalTest.Models
{
    public class Note : AuditEntity
    {
        [Key]
        public int Id { get; }

        public string NoteText { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }


    public class NoteLog : AuditEntity
    {
        [Key, Column(Order = 1)]
        public int AuditRowId { get; set; }
        public DateTime AuditCreatedOn { get; set; }
        public string AuditSqlCommand { get; set; }
        public int Id { get; }

        public string NoteText { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }

}
