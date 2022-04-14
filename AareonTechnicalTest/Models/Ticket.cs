using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AareonTechnicalTest.Models
{
    public class Ticket:AuditEntity
    {
        [Key]
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }
    }


    public class TicketLog : AuditEntity
    {
        [Key, Column(Order = 1)]
        public int AuditRowId { get; set; }
        public DateTime AuditCreatedOn { get; set; }
        public string AuditSqlCommand { get; set; }
        public int Id { get; }

        public string Content { get; set; }

        public int PersonId { get; set; }
    }

}
