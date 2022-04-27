using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public enum Progress { 
        Finish, Doing
    }
    public class Work
    {
        [Key]
        public int WorkID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name ="Work Name")]
        public string WorkName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Deadline")]
        public DateTime Deadline { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        public int SupervisorID { get; set; }
        public virtual Supervisor Supervisor { get; set; }

        public int BuilderID { get; set; }
        public virtual Builder Builder { get; set; }

        [DisplayFormat(NullDisplayText ="Start")]
        public Progress? Progress { get; set; }
        
    }
}