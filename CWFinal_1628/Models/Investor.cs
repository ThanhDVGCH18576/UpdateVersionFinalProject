using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CWFinal_1628.Models
{
    public class Investor:Person
    {
        public Investor()
        {
            Image = "~/Content/images/add.jpg";
        }
    }
}