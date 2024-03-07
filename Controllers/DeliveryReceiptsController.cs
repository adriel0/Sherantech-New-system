using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DeliveryReceiptsController : Controller
    {
        private readonly ILogger<DeliveryReceiptsController> _logger;


        public DeliveryReceiptsController(ILogger<DeliveryReceiptsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<DeliveryReceiptsModel> deliveryReceiptsModels = new List<DeliveryReceiptsModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM DeliveryReceipts";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeliveryReceiptsModel drm = new DeliveryReceiptsModel();
                                drm.drNo = reader.GetInt32(0);
                                drm.invoiceNo = reader.GetInt32(1);
                                drm.soldTo = reader.GetString(2);
                                drm.dateSold = reader.GetSqlDateTime(3).Value;
                                drm.terms = reader.GetString(4);


                                deliveryReceiptsModels.Add(drm);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(deliveryReceiptsModels);
        }
        [HttpGet]
        public IActionResult Edit(int drNo)
        {
            DeliveryReceiptsModel drm = new DeliveryReceiptsModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM DeliveryReceipts WHERE drNo = " + drNo;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                drm.invoiceNo = reader.GetInt32(1);
                                drm.soldTo = reader.GetString(2);
                                drm.dateSold = reader.GetSqlDateTime(3).Value;
                                drm.terms = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(drm);
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
                        command.CommandText = "UPDATE DeliverReceipts SET invoiceNo = @in, soldTo = @st, dateSold = @ds, terms = @t  WHERE drNo = @drNo";

                        command.Parameters.AddWithValue("@in", Request.Form["invoiceNo"].ToString());
                        command.Parameters.AddWithValue("@st", Request.Form["soldTo"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());
                        command.Parameters.AddWithValue("@t", Request.Form["terms"].ToString());

                        Debug.WriteLine(Request.Form["drNo"].ToString());
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
                        command.CommandText = "INSERT Into DeliveryReceipts (invoiceNo, soldTo, dateSold, terms, drNo) Values (@in, @st, @ds, @t, @drNo)";

                        command.Parameters.AddWithValue("@in", Request.Form["invoiceNo"].ToString());
                        command.Parameters.AddWithValue("@st", Request.Form["soldTo"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());
                        command.Parameters.AddWithValue("@t", Request.Form["terms"].ToString());
                        command.Parameters.AddWithValue("@drNo", Request.Form["drNo"].ToString());
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