using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReceivablesController : Controller
    {
        private readonly ILogger<ReceivablesController> _logger;


        public ReceivablesController(ILogger<ReceivablesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<ReceivablesModel> receivablesModels = new List<ReceivablesModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Receivables";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReceivablesModel rm = new ReceivablesModel();
                                rm.accountNo = reader.GetInt32(0);
                                rm.dealer = reader.GetString(1);
                                rm.bankName = reader.GetString(2);
                                rm.checkNo = reader.GetInt32(3);
                                rm.rtNo = reader.GetInt32(4);
                                rm.payToTheOrderOf = reader.GetString(5);
                                rm.dateIssued = reader.GetSqlDateTime(6).Value;
                                rm.dateDue = reader.GetSqlDateTime(7).Value;
                                rm.amount = reader.GetInt32(8);
                                rm.status = reader.GetString(9);
                                rm.remarks = reader.GetString(10);

                                receivablesModels.Add(rm);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(receivablesModels);
        }
        [HttpGet]
        public IActionResult Edit(int accountNo)
        {
            ReceivablesModel rm = new ReceivablesModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Receivables WHERE accountNo = " + accountNo;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rm.dealer = reader.GetString(1);
                                rm.bankName = reader.GetString(2);
                                rm.checkNo = reader.GetInt32(3);
                                rm.rtNo = reader.GetInt32(4);
                                rm.payToTheOrderOf = reader.GetString(5);
                                rm.dateIssued = reader.GetSqlDateTime(6).Value;
                                rm.dateDue = reader.GetSqlDateTime(7).Value;
                                rm.amount = reader.GetInt32(8);
                                rm.status = reader.GetString(9);
                                rm.remarks = reader.GetString(10);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(rm);
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
                        command.CommandText = "UPDATE Receivables SET dealer = @d, bankName = @bn, checkNo = @cn, " +
                                              "rtNo = @rn, payToTheOrderOf = @pttoo, dateIssued = @di, " +
                                              "dateDue = @dd, amount = @a, status = @s, remarks = @r WHERE accountNo = @accountNo";

                        command.Parameters.AddWithValue("@d", Request.Form["dealer"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["bankName"].ToString());
                        command.Parameters.AddWithValue("@cn", Request.Form["checkNo"].ToString());
                        command.Parameters.AddWithValue("@rn", Request.Form["rtNo"].ToString());
                        command.Parameters.AddWithValue("@pttoo", Request.Form["payToTheOrderOf"].ToString());
                        command.Parameters.AddWithValue("@di", Request.Form["dateIssued"].ToString());
                        command.Parameters.AddWithValue("@dd", Request.Form["dateDue"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["amount"].ToString());
                        command.Parameters.AddWithValue("@s", Request.Form["status"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());

                        Debug.WriteLine(Request.Form["accountNo"].ToString());
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
                        command.CommandText = "INSERT Into Receivables (dealer, bankName, checkNo, " +
                                              "rtNo, payToTheOrderOf, dateIssued, dateDue, amount, status, remarks, accountNo) " +
                                              "Values (@d, @bn, @cn, @rn, @pttoo, @di, @dd, @a, @s, @r, @accountNo)";

                        command.Parameters.AddWithValue("@d", Request.Form["dealer"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["bankName"].ToString());
                        command.Parameters.AddWithValue("@cn", Request.Form["checkNo"].ToString());
                        command.Parameters.AddWithValue("@rn", Request.Form["rtNo"].ToString());
                        command.Parameters.AddWithValue("@pttoo", Request.Form["payToTheOrderOf"].ToString());
                        command.Parameters.AddWithValue("@di", Request.Form["dateIssued"].ToString());
                        command.Parameters.AddWithValue("@dd", Request.Form["dateDue"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["amount"].ToString());
                        command.Parameters.AddWithValue("@s", Request.Form["status"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());
                        command.Parameters.AddWithValue("@accountNo", Request.Form["accountNo"].ToString());
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