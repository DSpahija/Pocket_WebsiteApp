using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Pocket_Website.Language
{
    public class Multilanguage
    {
        ResourceManager resourcesManager;
        
        public string description { get; set; }
        public string descriptionPocket { get; set; }
        public string descriptionDesktop { get; set; }
        public string descriptionMobile { get; set; }

        public string aboutus { get; set; }
        public string followus { get; set; }

        public string home { get; set; }
        public string homecare { get; set; }
        public string utilities { get; set; }
        public string wardrobe { get; set; }
        public string holidays { get; set; }
        public string medical { get; set; }
        public string loans { get; set; }
        public string savings { get; set; }
        public string emergency { get; set; }
        public string extra { get; set; }

        public string fullname { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string repeatpassword { get; set; }
        public string forgotpassword { get; set; }
        public string login { get; set; }
        public string signup { get; set; }
        public string longsignup { get; set; }
        public string longsignin { get; set; }

        public string welcome { get; set; }
        public string notes { get; set; }
        public string add { get; set; }
        public string typename { get; set; }
        public string newtypename { get; set; }
        public string costexpenses { get; set; }
        public string transactiondate { get; set; }
        public string paymentmethod { get; set; }
        public string totalincome { get; set; }
        public string totalloans { get; set; }
        public string budget { get; set; }
        public string reports { get; set; }
        public string language { get; set; }
        public string help { get; set; }
        public string logout { get; set; }
        
        public void ChangeLanguage(bool clicked, string language)
        {
            if (clicked == true && language == "Shqip")
            {
                resourcesManager = new ResourceManager("Pocket_Website.Language.lang_al", Assembly.GetExecutingAssembly());
            }
            else
            {
                resourcesManager = new ResourceManager("Pocket_Website.Language.lang_en", Assembly.GetExecutingAssembly());
            }
            ChangeLanguage();
        }

        public void ChangeLanguage()
        {
            //Descriptions
            description = resourcesManager.GetString("description");
            descriptionPocket = resourcesManager.GetString("descriptionPocket");
            descriptionMobile = resourcesManager.GetString("descriptionMobile");
            descriptionDesktop = resourcesManager.GetString("descriptionDesktop");

            //Extra
            aboutus = resourcesManager.GetString("aboutus");
            followus = resourcesManager.GetString("followus");

            //Signin & Sign up
            login = resourcesManager.GetString("login");
            signup = resourcesManager.GetString("signup");
            longsignup = resourcesManager.GetString("longsignup");
            longsignin = resourcesManager.GetString("longsignin");
            username = resourcesManager.GetString("username");
            password = resourcesManager.GetString("password");
            repeatpassword = resourcesManager.GetString("repeatpassword");
            forgotpassword = resourcesManager.GetString("forgotpassword");
            fullname = resourcesManager.GetString("fullname");
            email = resourcesManager.GetString("email");


            //GeneralChange
            language = resourcesManager.GetString("languagesswitch");
            reports = resourcesManager.GetString("reports");
            logout = resourcesManager.GetString("logout");
            help = resourcesManager.GetString("help");

            //PANELS
            welcome = resourcesManager.GetString("welcome");
            totalincome = resourcesManager.GetString("totalincome");
            totalloans = resourcesManager.GetString("totalloans");
            budget = resourcesManager.GetString("budget");
            homecare = resourcesManager.GetString("homecare");
            utilities = resourcesManager.GetString("utilities");
            wardrobe = resourcesManager.GetString("wardrobe");
            holidays = resourcesManager.GetString("holidays");
            medical = resourcesManager.GetString("medical");
            savings = resourcesManager.GetString("savings");
            emergency = resourcesManager.GetString("emergency");
            extra = resourcesManager.GetString("extra");
            typename = resourcesManager.GetString("typename");
            newtypename = resourcesManager.GetString("newtypename");
            costexpenses = resourcesManager.GetString("costexpenses");
            transactiondate = resourcesManager.GetString("transactiondate");
            paymentmethod = resourcesManager.GetString("paymentmethod");
            notes = resourcesManager.GetString("notes");
            add = resourcesManager.GetString("add");
        }


    }
}
