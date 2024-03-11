using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.Models.Purchases;

namespace WebApplication1.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly ILogger<PurchasesController> _logger;


        public PurchasesController(ILogger<PurchasesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            List<PurchasesModel> purchasesModels = new List<PurchasesModel>();
            List<PurchasesItemsModel> purchasesItemsModels = new List<PurchasesItemsModel>(); 
            PurchasesDisplayDataModel result = new PurchasesDisplayDataModel();

            if (id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT TOP 1 Id FROM Purchases ORDER BY Id DESC";
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

                    String sql = "SELECT * FROM Purchases";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchasesModel pm = new PurchasesModel();
                                pm.referenceNo = reader.GetInt32(0);
                                pm.purchasedFrom = reader.GetString(1);
                                pm.datePurchased = reader.GetSqlDateTime(2).Value;
                                pm.receivedBy = reader.GetString(3);
                                pm.closed = reader.GetBoolean(4);

                                purchasesModels.Add(pm);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.Details = purchasesModels;

            result.CurrentItems = new PurchasesItemsModel();
            result.CurrentItems.Id = id;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Purchases WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.CurrentItems.Id = reader.GetInt32(0);
                                result.CurrentItems.qty = reader.GetInt32(1);
                                result.CurrentItems.unit = reader.GetString(2);
                                result.CurrentItems.stockNo = reader.GetInt32(3);
                                result.CurrentItems.unitPrice = reader.GetInt32(4);
                                result.CurrentItems.amount = reader.GetInt32(5);
                                result.CurrentItems.remarks = reader.GetString(6);
                                
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            return View(purchasesModels);
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
                        command.CommandText = "UPDATE Purchases SET referenceNo = @rn, purchasedFrom = @pf, datePurchased = @dp, receivedBy = @rb, closed = @c " +
                                              "WHERE id = @id";

                        command.Parameters.AddWithValue("@rn", Request.Form["referenceNo"].ToString());
                        command.Parameters.AddWithValue("@pf", Request.Form["purchasedFrom"].ToString());
                        command.Parameters.AddWithValue("@dp", Request.Form["datePurchased"].ToString());
                        command.Parameters.AddWithValue("@rb", Request.Form["receivedBy"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["closed"].ToString());
                        command.Parameters.AddWithValue("@id", id);
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

        
        public IActionResult Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into Purchases (purchasedFrom, datePurchased, receivedBy, closed, referenceNo) " +
                                              "Values (@pf, @dp, @rb, @c, @referenceNo)";

                        command.Parameters.AddWithValue("@pf", Request.Form["purchasedFrom"].ToString());
                        command.Parameters.AddWithValue("@dp", Request.Form["datePurchased"].ToString());
                        command.Parameters.AddWithValue("@rb", Request.Form["receivedBy"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["closed"].ToString());
                        command.Parameters.AddWithValue("@referenceNo", Request.Form["referenceNo"].ToString());
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