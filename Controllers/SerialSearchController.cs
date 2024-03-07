using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SerialSearchController : Controller
    {
        private readonly ILogger<SerialSearchController> _logger;


        public SerialSearchController(ILogger<SerialSearchController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<SerialSearchModel> serialSearchModels = new List<SerialSearchModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM SerialSearch";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SerialSearchModel ssm = new SerialSearchModel();

                                ssm.serialNo = reader.GetInt32(0);
                                ssm.drNo = reader.GetInt32(1);
                                ssm.stockName = reader.GetString(2);
                                ssm.customer = reader.GetString(3);
                                ssm.dateSold = reader.GetSqlDateTime(4).Value;

                                serialSearchModels.Add(ssm);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(serialSearchModels);
        }
        [HttpGet]
        public IActionResult Edit(int serialNo)
        {
            SerialSearchModel ssm = new SerialSearchModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM SerialSearch WHERE serialNo = " + serialNo;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ssm.drNo = reader.GetInt32(1);
                                ssm.stockName = reader.GetString(2);
                                ssm.customer = reader.GetString(3);
                                ssm.dateSold = reader.GetSqlDateTime(4).Value;

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(ssm);
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
                        command.CommandText = "UPDATE SerialSearch SET drNo = @dn, stockName = @sn, customer = @c, dateSold = @ds " +
                                              "WHERE serialNo = @serialNo";

                        command.Parameters.AddWithValue("@dn", Request.Form["drNo"].ToString());
                        command.Parameters.AddWithValue("@sn", Request.Form["stockName"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["customer"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());

                        Debug.WriteLine(Request.Form["serialNo"].ToString());
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
                        command.CommandText = "INSERT Into SerialSearch (drNo, stockName, customer, dateSold, serialNo) " +
                                              "Values (@dn, @sn, @c, @ds, @serialNo)";

                        command.Parameters.AddWithValue("@dn", Request.Form["drNo"].ToString());
                        command.Parameters.AddWithValue("@sn", Request.Form["stockName"].ToString());
                        command.Parameters.AddWithValue("@c", Request.Form["customer"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());
                        command.Parameters.AddWithValue("@serialNo", Request.Form["serialNo"].ToString());
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