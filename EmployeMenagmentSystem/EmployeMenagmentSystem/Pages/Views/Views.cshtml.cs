using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace IdeaTrading.Pages.Views
{
    public class ViewsModel : PageModel
    {
                 
        public List<EmployeeInfo> listEmployees = new List<EmployeeInfo>();


        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=testDb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "SELECT * FROM Useer";
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            while (reader.Read()) 
                            {
                                
                                EmployeeInfo employeeInfo = new EmployeeInfo();
                                
                                employeeInfo.id = "" + reader.GetInt32(0);
                                employeeInfo.firstName = reader.GetString(1);
                                employeeInfo.lastName = reader.GetString(2);
                                employeeInfo.address = reader.GetString(3);
                                employeeInfo.city = reader.GetString(4);
                                employeeInfo.dateOfEmployment ="" + reader.GetDateTime(5);
                                employeeInfo.position = reader.GetString(6);
                                employeeInfo.phone = reader.GetString(7);
                             
                                listEmployees.Add(employeeInfo);
                            }
                        }
                    }
                }
            }

            catch (Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }


    public class EmployeeInfo
    {
        public String id;
        public String firstName;
        public String lastName;
        public String address;
        public String city;
        public String dateOfEmployment;
        public String position;
        public String phone;
        
    }
}
