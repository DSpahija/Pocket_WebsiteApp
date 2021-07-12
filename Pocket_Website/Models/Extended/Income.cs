using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pocket_Website.Models.Extended
{
    public class Income
    {
        public int IDIncome { get; set; }
        public int IDUser { get; set; }

        [Display(Name = "Name")]
        public string PersonName { get; set; }

        [Display(Name = "Total Income")]
        public double TotalIncome { get; set; }

        [Display(Name = "Salary Date")]
        public System.DateTime SalaryDate { get; set; }
    }
}