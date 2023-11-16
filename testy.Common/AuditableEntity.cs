using System;
using System.ComponentModel.DataAnnotations;

namespace testy.Common
{
    public abstract class AuditableEntity
    {
        [StringLength(1)]
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }

        [StringLength(40)]
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        [StringLength(40)]
        public string UpdatedBy { get; set; }
    }
}
