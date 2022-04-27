using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public class AssignProject
    {
        [Key]
        public int AssignID { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        public int BuilderID { get; set; }
        public virtual Builder Builder { get; set; }

        public int SupervisorID { get; set; }
        public virtual Supervisor Supervisor { get; set; }
    }
}