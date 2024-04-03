using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models.Dealers;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        //hash stuff
        /*
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        */

        [HttpPost]
        public IActionResult Login_Post()
        {
            String username = "admin";
            String password = "Sherantech";
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    connection.Open();
                    String sql = "SELECT Password FROM Accounts WHERE Username = " + Request.Form["Username"].ToString();
                    

                    
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                password = reader.GetString(0);

                            }
                        }
                    }

                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            //hash
            // if (password.Equals(GetHashString(Request.Form["Password"].ToString())))
            if (username.Equals(Request.Form["Username"].ToString()) && password.Equals(Request.Form["Password"].ToString()))
            {
                return RedirectToRoute(new
                {
                    controller = "dealers",
                    action = "index"
                });
            }
            return RedirectToRoute(new
            {
                controller = "home",
                action = "login"
            });
        }

        public IActionResult Dealers()
        {
            List<DealersModel> dealersModels = new List<DealersModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Dealers";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(dealersModels);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            DealersModel dealersModels = new DealersModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Dealers WHERE Id = "+id;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //dealersModels.Id = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(dealersModels);
        }
        [HttpPost]
        public IActionResult Edit()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Dealers SET DealerName = @dn, DealerAddress = @da, DealerAccountId = @dai  WHERE Id = @id";
 
                        command.Parameters.AddWithValue("@dn", Request.Form["DealerName"].ToString());
                        command.Parameters.AddWithValue("@da", Request.Form["DealerAddress"].ToString());
                        command.Parameters.AddWithValue("@dai", Request.Form["DealerAccountId"].ToString());
                        command.Parameters.AddWithValue("@id", Request.Form["Id"].ToString());
                        Debug.WriteLine(Request.Form["Id"].ToString());
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Addpost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into Dealers (DealerName, DealerAddress, DealerAccountId) Values (@dn, @da, @dai)";

                        command.Parameters.AddWithValue("@dn", Request.Form["DealerName"].ToString());
                        command.Parameters.AddWithValue("@da", Request.Form["DealerAddress"].ToString());
                        command.Parameters.AddWithValue("@dai", Request.Form["DealerAccountId"].ToString());
                        command.Parameters.AddWithValue("@id", Request.Form["Id"].ToString());
                        Debug.WriteLine(Request.Form["Id"].ToString());
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}