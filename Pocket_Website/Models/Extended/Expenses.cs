using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pocket_Website.Models
{
    public partial class Expenses
    {
        public int IDExpenses { get; set; }

        public int IDUser { get; set; }

        [Display(Name = "Category")]
        public int ExpensesCategory { get; set; }
        public string ExpensesCategoryName { get; set; }

        [Display(Name = "Type")]
        public int ExpensesType { get; set; }
        public string ExpensesTypeName { get; set; }

        [Display(Name = "Total")]
        public string TotalExpenses { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> ExpensesDate { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Payed")]
        public bool Payed { get; set; }

        public readonly List<ExpensesCategory> _categories;
        [Display(Name = "Category Name")]
        public int SelectedCategoryId { get; set; }

        public virtual IEnumerable<SelectListItem> CategoryName
        {
            get
            {
                return new SelectList(_categories, "IDExpensesCategory", "TypeCategory");
            }
        }
    }
}