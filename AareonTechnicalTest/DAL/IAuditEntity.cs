using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AareonTechnicalTest.DAL
{
    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }

        void SetCreator(string creator, DateTime dateTime);

        void SetUpdater(string updater, DateTime dateTime);
    }
}
