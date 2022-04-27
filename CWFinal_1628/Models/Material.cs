using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name ="Material Name")]
        public string MaterialName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Material Code")]
        public string MaterialCode { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [ForeignKey("MaterialType")]
        public int MaterialTypeID { get; set; }

        public virtual MaterialType MaterialType { get; set; }
        public virtual ICollection<ProjectMaterial> ProjectMaterials { get; set; }
    }
}