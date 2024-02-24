using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DealersController : Controller
    {
        private readonly ILogger<DealersController> _logger;


        public DealersController(ILogger<DealersController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
                                DealersModel dm = new DealersModel();
                                dm.Id = reader.GetInt32(0);
                                dm.DealerName = reader.GetString(1);
                                dm.DealerAddress = reader.GetString(2);
                                dm.DealerAccountId = reader.GetInt32(3);
                                dealersModels.Add(dm);

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

                    String sql = "SELECT * FROM Dealers WHERE Id = " + id;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                dealersModels.Id = reader.GetInt32(0);
                                dealersModels.DealerName = reader.GetString(1);
                                dealersModels.DealerAddress = reader.GetString(2);
                                dealersModels.DealerAccountId = reader.GetInt32(3);
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