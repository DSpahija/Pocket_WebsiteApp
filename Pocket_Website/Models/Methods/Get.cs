using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pocket_Website.Models
{
    public class Get
    {
        Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1();

        public object HelperDB { get; private set; }

        public int GetIDUser(string email)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var iduser = databaseEntities.Users.FirstOrDefault(a => a.Email == email);
                return iduser.IDUser;
            }
        }

        public int GetTypeID(string type)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var types = databaseEntities.ExpensesTypes.FirstOrDefault(a => a.TypeName == type);
                return types.IDExpensesType;
            }
        }

        public int GetCategoryID(string category)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var categories = databaseEntities.ExpensesCategories.FirstOrDefault(a => a.TypeCategory == category);
                return categories.IDExpensesCategory;
            }
        }

        public string GetFullName(string email)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var fullname = databaseEntities.Users.FirstOrDefault(a => a.Email == email);
                return fullname.FullName;
            }
        }

        Helper helper = new Helper();

        public List<Expenses> GetExpenses(string email)
        {
            var listExpenses = new List<Expenses>();
            int IDUser = GetIDUser(email);


            using (SqlConnection connection = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Pocket_Database.mdf"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.ExpensesUser(@id)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", IDUser);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var expense = new Expenses();
                        expense.ExpensesCategoryName = reader["ExpensesCategory"].ToString();
                        expense.ExpensesTypeName = reader["ExpensesType"].ToString();
                        expense.ExpensesDate = (DateTime)reader["ExpensesDate"];
                        expense.PaymentMethod = reader["PaymentMethod"].ToString();
                        expense.TotalExpenses = reader["TotalExpenses"] + "€";
                        expense.Notes = reader["Notes"].ToString();
                        listExpenses.Add(expense);
                    }
                }
                connection.Close();
                return listExpenses;
            }
        }

        //INCOMES
        public List<Income> GetIncomes(string email)
        {
            var listIncomes = new List<Income>();
            int IDUser = GetIDUser(email);

            using (SqlConnection connection = new SqlConnection(@"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Pocket_Database.mdf"))
            {
                connection.Open();
                string query = "SELECT * FROM dbo.IncomesUser(@id)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", IDUser);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var income = new Income();
                        income.PersonName = reader["PersonName"].ToString();
                        income.TotalIncome = Convert.ToDouble(reader["TotalIncome"]);
                        income.SalaryDate = (DateTime)reader["SalaryDate"];
                        listIncomes.Add(income);
                    }
                }
                connection.Close();
                return listIncomes;
            }
        }

        //CATEGORIES NAME
        public string CategoryName(int id)
        {
            using(Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "dbo.CategoryName(@id)";
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteScalar().ToString();
            }
        }        

        //EXPENSES TOTAL FOR CHART
        public double HomeCareTotal(int ID)
        { 
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total=0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 1 && a.IDUser == ID))
                {
                    total += item.TotalExpenses;
                }
                return total;
            }
        }
        public double UtilitiesTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 2 && a.IDUser == ID))
                {
                    total += item.TotalExpenses;
                }
                return total;
            }
        }

        public double WardrobeTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 3 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }
        public double HolidaysTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 4 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }
        public double MedicalTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 5 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }
        public double LoansTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 6 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }
        public double SavingsTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 7 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }

        public double EmergencyTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 8 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }

        public double ExtraTotal(int ID)
        {
            using (Pocket_DatabaseEntities1 databaseEntities = new Pocket_DatabaseEntities1())
            {
                double total = 0;
                foreach (var item in databaseEntities.Expenses.Where(a => a.ExpensesCategory == 9 && a.IDUser == ID))
                {
                    total = item.TotalExpenses;
                }
                return total;
            }
        }
    }
}