using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;
using WebApplication1.Models.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;


        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            List<InventoryModel> inventoryModels = new List<InventoryModel>();
            InventoryDisplayDataModel result = new InventoryDisplayDataModel();

            if (id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT TOP 1 Id FROM Inventory ORDER BY Id DESC";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    id = reader.GetInt32(0);

                                }
                            }
                        }

                    }
                }
                catch (SqlException e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }



            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT inv.id,inv.name,inv.description,inv.category,inv.unitPrice FROM Inventory as inv";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                InventoryModel im = new InventoryModel();
                                im.Id = reader.GetInt32(0);
                                im.name = reader.GetString(1);
                                im.description = reader.GetString(2);
                                im.category = reader.GetString(3);
                                im.unitPrice = reader.GetInt64(4);


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
            result.Data = inventoryModels;


            result.CurrentDetails = new InventoryDetailsModel();
            result.CurrentDetails.Id = id;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT inv.id,inv.name,inv.description,inv.category,inv.unitPrice,inv.unit,inv.qtyPerBox,inv.Supplier,inv.hasSerial FROM Inventory as inv WHERE inv.id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.CurrentDetails.Id = reader.GetInt32(0);
                                result.CurrentDetails.name = reader.GetString(1);
                                result.CurrentDetails.description = reader.GetString(2);
                                result.CurrentDetails.category = reader.GetString(3);
                                result.CurrentDetails.unitPrice = reader.GetInt64(4);
                                result.CurrentDetails.unit = reader.GetInt64(5);
                                result.CurrentDetails.qtyPerBox = reader.GetInt64(6);
                                result.CurrentDetails.supplier = reader.GetInt32(7);
                                result.CurrentDetails.hasSerial = reader.GetBoolean(8);
                                
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            List<SelectListItem> dl = new List<SelectListItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT id,DealerBusinessName FROM Dealers";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem d = new SelectListItem();
                                d.Value = reader.GetInt32(0).ToString();
                                d.Text = reader.GetString(1);
                                dl.Add(d);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.dealers = dl;

            return View(result);
        }
        
        [HttpPost]
        public IActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Inventory SET name = @n, description = @d, category = @c, unitPrice = @up WHERE id = @id";

                        command.Parameters.AddWithValue("@n", Request.Form["name"].ToString());
                        command.Parameters.AddWithValue("@d", Request.Form["description"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["category"].ToString());
                        command.Parameters.AddWithValue("@up", Request.Form["unitPrice"].ToString());
                        command.Parameters.AddWithValue("@id", id);
                        Debug.WriteLine(Request.Form["id"].ToString());
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

        public IActionResult Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into Inventory (description, category, unitPrice, name) Values (@d, @c, @up, @n)";

                        command.Parameters.AddWithValue("@d", "");
                        command.Parameters.AddWithValue("@c", "");
                        command.Parameters.AddWithValue("@up", "");
                        command.Parameters.AddWithValue("@n", "");
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