using Pocket_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pocket_Website.Controllers
{
    public class UserController : BaseController
    {     
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "isEmailVerified,ActivationCode," +
            "InsertedDate,LastUpdatedDate,LastUpdatedBy,UpdateCounter")] User user)
        {
            bool status = false;
            string message = "";

            if (ModelState.IsValid)
            {
                #region Email exists
                var emailExist = EmailExist(user.Email);
                if (emailExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                }
                #endregion

                #region Generate activation code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Encrypt
                user.Password = Encrypt.Encrypt.EncryptPassword(user.Password);
                user.ConfirmPassword = Encrypt.Encrypt.EncryptPassword(user.ConfirmPassword);
                #endregion
                user.isEmailVerified = false;

                #region Save to Database
                using(Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
                {
                    databaseEntities.Users.Add(user);
                    databaseEntities.SaveChanges();

                    //Send email to user
                    SendVerificationEmail(user.Email, user.ActivationCode.ToString());
                    message = "Registration successfully done. Account activation link " +
                        "has been sent to your email: " + user.Email + ".";
                    status = true;
                }
                #endregion
            }
            else
            {
                message = "Invalid Request";
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View(user);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool status = false;
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                databaseEntities.Configuration.ValidateOnSaveEnabled = false;
                var code = databaseEntities.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if(code != null)
                {
                    code.isEmailVerified = true;
                    databaseEntities.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.Message = "Invalid Request.";
                }
            }
            ViewBag.Status = status;
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string returnUrl)
        {
            string message = "";
            using(Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var email = databaseEntities.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                if (email != null)
                {
                    if (string.Compare(Encrypt.Encrypt.EncryptPassword(login.Password), email.Password) == 0)
                    {
                        int timeout = login.rememberMe ? 525600 : 20; //525600 = 1 year
                        var ticket = new FormsAuthenticationTicket(login.Email, login.rememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "UserHome");
                        }
                    }
                    else
                    {
                        message = "Invalid credentials provided.";
                    }
                }
                else
                {
                    message = "Invalid credentials provided.";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [NonAction]
        public bool EmailExist(string email)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var v = databaseEntities.Users.Where(a => a.Email == email.FirstOrDefault().ToString());
                return v != null;
            }
        }

        [NonAction]
        public void SendVerificationEmail(string email, string activationCode)
        {
            var verifyUrl = "/User/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var fromEmail = new MailAddress("pocket.noreply@gmail.com", "POCKET");
            var toEmail = new MailAddress(email);
            var fromEmailPassword = "pocket123!";

            string subject = "Welcome to Pocket!";

            string body = "</br></br> Hi there, Welcome! To start discovering the world of Pocket, " +
                "please verify your account. </br> Click on the below link to verify your account!" +
                "</br> <a href='" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }

    }
}