using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceFile = "~/Files/";
        }

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Invoice Name")]
        public string InvoiceName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Invoice File")]
        public string InvoiceFile { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Submission Date")]
        public DateTime DateSubmit { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public float Total { get; set; }

        [ForeignKey("Project")]
        public int ProjectID { get; set; }
        public virtual Project Project { get; set; }

        [NotMapped]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}