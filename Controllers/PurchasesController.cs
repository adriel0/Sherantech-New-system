using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly ILogger<PurchasesController> _logger;


        public PurchasesController(ILogger<PurchasesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<PurchasesModel> purchasesModels = new List<PurchasesModel>();
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
            return View(purchasesModels);
        }
        [HttpGet]
        public IActionResult Edit(int referenceNo)
        {
            PurchasesModel pm = new PurchasesModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Purchases WHERE referenceNo = " + referenceNo;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                pm.purchasedFrom = reader.GetString(1);
                                pm.purchasedFrom = reader.GetString(2);
                                pm.datePurchased = reader.GetSqlDateTime(3).Value;
                                pm.receivedBy = reader.GetString(4);
                                pm.closed = reader.GetBoolean(5);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(pm);
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
                        command.CommandText = "UPDATE Purchases SET purchasedFrom = @pf, datePurchased = @dp, receivedBy = @rb, closed = @c " +
                                              "WHERE referenceNo = @referenceNo";

                        command.Parameters.AddWithValue("@pf", Request.Form["purchasedFrom"].ToString());
                        command.Parameters.AddWithValue("@dp", Request.Form["datePurchased"].ToString());
                        command.Parameters.AddWithValue("@rb", Request.Form["receivedBy"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["closed"].ToString());

                        Debug.WriteLine(Request.Form["referenceNo"].ToString());
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