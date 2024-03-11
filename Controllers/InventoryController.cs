using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;
using WebApplication1.Models.Inventory;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;


        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<InventoryModel> inventoryModels = new List<InventoryModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Inventory";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InventoryModel im = new InventoryModel();
                                im.name = reader.GetString(0);
                                im.description = reader.GetString(1);
                                im.category = reader.GetString(2);
                                im.unitPrice = reader.GetInt32(3);


                                inventoryModels.Add(im);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(inventoryModels);
        }
        [HttpGet]
        public IActionResult Edit(string name)
        {
            InventoryModel im = new InventoryModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Inventory WHERE name = " + name;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                im.description = reader.GetString(1);
                                im.category = reader.GetString(2);
                                im.unitPrice = reader.GetInt32(3);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(im);
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
                        command.CommandText = "UPDATE Inventory SET description = @d, category = @c, unitPrice = @up WHERE name = @name";

                        command.Parameters.AddWithValue("@d", Request.Form["description"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["category"].ToString());
                        command.Parameters.AddWithValue("@up", Request.Form["unitPrice"].ToString());

                        Debug.WriteLine(Request.Form["name"].ToString());
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
                        command.CommandText = "INSERT Into Inventory (description, category, unitPrice, name) Values (@d, @c, @up, @name)";

                        command.Parameters.AddWithValue("@d", Request.Form["description"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["category"].ToString());
                        command.Parameters.AddWithValue("@up", Request.Form["unitPrice"].ToString());
                        command.Parameters.AddWithValue("@name", Request.Form["name"].ToString());
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