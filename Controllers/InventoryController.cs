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

                    String sql = "SELECT inv.id,inv.name,inv.description,c.category,inv.unitPrice FROM Inventory as inv INNER JOIN category as c on c.id=inv.category";

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

                    String sql = "SELECT inv.id,inv.name,inv.description,c.category,inv.unitPrice,inv.unit,inv.qtyPerBox,d.name,inv.hasSerial FROM Inventory as inv INNER JOIN category as c on c.id=inv.category INNER JOIN dealers as d on d.id=inv.supplier WHERE id=@id";

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
                                result.CurrentDetails.unitPrice = reader.GetInt32(4);
                                result.CurrentDetails.unit = reader.GetString(5);
                                result.CurrentDetails.qtyPerBox = reader.GetInt32(6);
                                result.CurrentDetails.supplier = reader.GetString(7);
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

            return View(inventoryModels);
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