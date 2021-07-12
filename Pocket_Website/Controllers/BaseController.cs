using Pocket_Website.Language;
using Pocket_Website.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Pocket_Website.Controllers
{
    public class BaseController : Controller
    {
        Get getUserInfo = new Get();

        public BaseController()
        {
        }

        [NonAction]
        public string GetFullName(string email)
        {
            ViewBag.FullName = getUserInfo.GetFullName(email);
            return ViewBag.FullName;
        }
        
        public ActionResult ChangeLanguage(string Language)
        {
            if (Language != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Language);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = Language;
            Response.Cookies.Add(cookie);

            return View("Index");
        }
    }
}