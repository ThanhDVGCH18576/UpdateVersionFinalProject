using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public enum TypeProject
    {
        NewBuild, Repair
    }
    public class Project
    {
        [Key]
        public int ProjetcID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage ="Project Name not more than 50 character!", MinimumLength =3)]
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Location")]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name ="Start Date")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [StringLength(200)]
        [Display(Name="Description")]
        public string Description { get; set; }

        [ForeignKey("Investor")]
        public int InvestorID { get; set; }
        public  virtual Investor Investor { get; set; }


        [ForeignKey("Salesman")]
        public int SalesmanID { get; set; }
        public virtual Salesman Salesman { get; set; }

        [DisplayFormat(NullDisplayText = "Undefined")]
        public TypeProject? TypeProject { get; set; }


        public virtual ICollection<AssignProject> AssignProjects { get; set; }
        public virtual ICollection<ProjectMaterial> ProjectMaterials { get; set; }
        public virtual ICollection<Work> Works { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}