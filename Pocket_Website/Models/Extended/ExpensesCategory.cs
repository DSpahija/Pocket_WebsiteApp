using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pocket_Website.Models.Extended
{
    public class ExpensesCategory
    {
        public int IDExpensesCategory { get; set; }
        public string TypeCategory { get; set; }

    }
}