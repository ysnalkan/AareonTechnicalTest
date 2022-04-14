using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AareonTechnicalTest.Models
{
    public class Person : AuditEntity
    {
        [Key]
        public int Id { get; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }


    public class PersonLog : AuditEntity
    {
        [Key, Column(Order = 1)]
        public int AuditRowId { get; set; }
        public DateTime AuditCreatedOn { get; set; }
        public string AuditSqlCommand { get; set; }
        public int Id { get; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public bool IsAdmin { get; set; }
    }

}
