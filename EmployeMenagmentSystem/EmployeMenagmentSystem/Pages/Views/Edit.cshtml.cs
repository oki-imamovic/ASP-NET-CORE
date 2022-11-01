using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IdeaTrading.Pages.Views
{
    public class EditModel : PageModel
    {

        public EmployeeInfo employeeInfo = new EmployeeInfo();

        public String errorMessages = "";
        public String successMessages = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();

                    String sql = "SELECT * FROM Useer WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.firstName = reader.GetString(1);
                                employeeInfo.lastName = reader.GetString(2);
                                employeeInfo.address = reader.GetString(3);
                                employeeInfo.city = reader.GetString(4);
                                employeeInfo.dateOfEmployment = "" + reader.GetDateTime(5);
                                employeeInfo.position = reader.GetString(6);
                                employeeInfo.phone = reader.GetString(7);

                            }
                        }
                    }
                }
            }

            catch (Exception ex) 
            {
                errorMessages = ex.Message;
            }

        }

        public void OnPost() 
        {
            employeeInfo.id = Request.Form["id"];
            employeeInfo.firstName = Request.Form["firstName"];
            employeeInfo.lastName = Request.Form["lastName"];
            employeeInfo.address = Request.Form["address"];
            employeeInfo.city = Request.Form["city"];
            employeeInfo.dateOfEmployment = Request.Form["dateOfEmployment"];
            employeeInfo.position = Request.Form["position"];
            employeeInfo.phone = Request.Form["phone"];

            if (employeeInfo.id.Length == 0 || employeeInfo.firstName.Length == 0 || employeeInfo.lastName.Length == 0 ||
                employeeInfo.address.Length == 0 || employeeInfo.city.Length == 0 || employeeInfo.dateOfEmployment.Length == 0 || 
                employeeInfo.position.Length == 0 || employeeInfo.phone.Length == 0)
            {
                errorMessages = "All fields are required!";
                return;
            }


            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=dbTest;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "UPDATE Useer " + "SET firstName=@firstName, lastName=@lastName, address=@address, city=@city, dateOfEmployment=@dateOfEmployment, position=@position, phone=@phone WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@firstName", employeeInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", employeeInfo.lastName);
                        command.Parameters.AddWithValue("@address", employeeInfo.address);
                        command.Parameters.AddWithValue("@city", employeeInfo.city);
                        command.Parameters.AddWithValue("@dateOfEmployment", employeeInfo.dateOfEmployment);
                        command.Parameters.AddWithValue("@position", employeeInfo.position);
                        command.Parameters.AddWithValue("@phone", employeeInfo.phone);
                        command.Parameters.AddWithValue("@id", employeeInfo.id);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex) 
            {
                errorMessages=ex.Message;
                return;
            }

            successMessages = "Employee Edited Correctly";
            Response.Redirect("/Views/Views");

        }
    }
}
