using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public class MaterialType
    {
        [Key]
        public int MaterialTypeID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage ="Type material name is not more than 30 character!")]
        [Display(Name ="Material Type Name")]
        public string TypeName { get; set; }
    }
}