using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public enum Status
    {
        Agree, Disagree
    }
    public class ProjectMaterial
    {
        [Key]
        public int ProjectMaterialID { get; set; }

        [ForeignKey("Material")]
        public int MaterialID { get; set; }
        public virtual Material Material { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(20), MinLength(1)]
        [Display(Name ="Quantity")]
        public string Quantity { get; set; }

        [StringLength(200)]
        [Display(Name = "Comment")]
        public string Comment { get; set; }

        [DisplayFormat(NullDisplayText = "Progress")]
        public Status? Status { get; set; }
    }
}