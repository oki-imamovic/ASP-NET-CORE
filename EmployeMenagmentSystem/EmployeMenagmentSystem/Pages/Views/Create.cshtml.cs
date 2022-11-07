using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IdeaTrading.Pages.Views
{
    public class CreateModel : PageModel
    {
        public EmployeeInfo employeeInfo = new EmployeeInfo();

        public String errorMessages = "";
        public String successMessages = "";
        public void OnGet()
        {
        }

        public void OnPost() 
        {
            employeeInfo.firstName = Request.Form["firstName"];
            employeeInfo.lastName = Request.Form["lastName"];
            employeeInfo.address = Request.Form["address"];
            employeeInfo.city = Request.Form["city"];
            employeeInfo.dateOfEmployment = Request.Form["dateOfEmployment"];
            employeeInfo.position = Request.Form["position"];
            employeeInfo.phone = Request.Form["phone"];

            if (employeeInfo.firstName.Length == 0 || employeeInfo.lastName.Length == 0 || employeeInfo.address.Length == 0 ||
                employeeInfo.city.Length == 0 || employeeInfo.dateOfEmployment.Length == 0 || employeeInfo.position.Length == 0 ||
                employeeInfo.phone.Length == 0) 
            {
                errorMessages = "All the fields are requiered!";
                return;
            }

            // save the new employee into the db
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=testDb;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO Useer " +
                         "(firstName, lastName, address, city, dateOfEmployment, position, phone) VALUES " +
                         "(@firstName, @lastName, @address, @city, @dateOfEmployment, @position, @phone);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@firstName", employeeInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", employeeInfo.lastName);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@city", employeeInfo.city);
                        command.Parameters.AddWithValue("@dateOfEmployment", employeeInfo.dateOfEmployment);
                        command.Parameters.AddWithValue("@position", employeeInfo.position);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessages = ex.Message;
                return;
            }

            employeeInfo.firstName = "";
            employeeInfo.lastName = "";
            employeeInfo.address = "";
            employeeInfo.city = "";
            employeeInfo.dateOfEmployment = "";
            employeeInfo.position = "";
            employeeInfo.phone = "";

            successMessages = "New Employee Added Correctly";

            Response.Redirect("/Views/Views");
        }
    }
}
