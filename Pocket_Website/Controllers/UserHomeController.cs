using Pocket_Website.Models;
using Pocket_Website.Models.Methods;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pocket_Website.Controllers
{
    public class UserHomeController : BaseController
    {
        Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1();
        Get getUserInfo = new Get();

        [Authorize]
        public ActionResult Index()
        {
            GetFullName(HttpContext.User.Identity.Name);
            return View();
        }

        public ActionResult Expenses()
        {
            getUserInfo.GetExpenses(HttpContext.User.Identity.Name);
            return View(getUserInfo.GetExpenses(HttpContext.User.Identity.Name));
        }

        public ActionResult Incomes()
        {
            getUserInfo.GetIncomes(HttpContext.User.Identity.Name);
            return View(getUserInfo.GetIncomes(HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult AddExpense(Expens expense)
        {
            databaseEntities.Expenses.Add(expense).IDUser = getUserInfo.GetIDUser(HttpContext.User.Identity.Name);
            databaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddExpense()
        {
            Expens ex = new Expens();
            return View();
        }

        int categoryID, typeID;
        public ActionResult EditExpense(string category, string type, DateTime date)
        {
            categoryID = getUserInfo.GetCategoryID(category);
            typeID = getUserInfo.GetTypeID(type);
            return View(databaseEntities.Expenses.Where(a => a.ExpensesCategory == categoryID && a.ExpensesType == typeID && a.ExpensesDate == date).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditExpense(Expens expenses)
        {
            databaseEntities.Entry<Expens>(expenses).State = EntityState.Modified;
            databaseEntities.SaveChanges();
            return View("Index");
        }

        public ActionResult DeleteExpense(string category, string type, DateTime date)
        {
            categoryID = getUserInfo.GetCategoryID(category);
            typeID = getUserInfo.GetTypeID(type);
            var expense = databaseEntities.Expenses.Where(a => a.ExpensesCategory == categoryID && a.ExpensesType == typeID && a.ExpensesDate == date).FirstOrDefault();
            databaseEntities.Expenses.Remove(expense);
            databaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditIncome(string personName, string totalIncome)
        {
            return View(databaseEntities.Incomes.Where(a => a.PersonName == personName && a.TotalIncome == Convert.ToDouble(totalIncome)).FirstOrDefault());
        }


        [HttpPost]
        public ActionResult EditIncome(Income income)
        {
            databaseEntities.Entry<Income>(income).State = EntityState.Modified;
            databaseEntities.SaveChanges();
            return View("Index");
        }


        float totalincome;
        int idUser;
        public ActionResult DeleteIncome(string personname, string totalincome, DateTime date)
        {
            idUser = getUserInfo.GetIDUser(personname);
            //totalincome = getUserInfo.GetTotalIncome(totalincome);
            var expense = databaseEntities.Expenses.Where(a => a.ExpensesCategory == categoryID && a.ExpensesType == typeID && a.ExpensesDate == date).FirstOrDefault();
            databaseEntities.Expenses.Remove(expense);
            databaseEntities.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetDropdownListTypes(int? value)
        {
            List<string> dataTypes = new List<string>();

            foreach (var item in databaseEntities.ExpensesTypes.Where(a => a.TypeCategory == value))
            {
                dataTypes.Add(item.TypeName);
            }
            return Json(new { data = dataTypes }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChartData()
        {
            idUser = getUserInfo.GetIDUser(HttpContext.User.Identity.Name);

            ExpensesData data = new ExpensesData();
            data.totalHomecare = getUserInfo.HomeCareTotal(idUser);
            data.totalUtilities = getUserInfo.UtilitiesTotal(idUser);
            data.totalWardrobe = getUserInfo.WardrobeTotal(idUser);
            data.totalHolidays = getUserInfo.HolidaysTotal(idUser);
            data.totalMedical = getUserInfo.MedicalTotal(idUser);
            data.totalEmergency = getUserInfo.EmergencyTotal(idUser);
            data.totalExtra = getUserInfo.ExtraTotal(idUser);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}