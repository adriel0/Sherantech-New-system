using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;
using WebApplication1.Models.Dealers;
using WebApplication1.Models.Purchases;
using WebApplication1.Models.Receivables;

namespace WebApplication1.Controllers
{
    public class ReceivablesController : Controller
    {
        private readonly ILogger<ReceivablesController> _logger;


        public ReceivablesController(ILogger<ReceivablesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            List<ReceivablesModel> receivablesModels = new List<ReceivablesModel>();
            List<ReferenceNoModel> referenceNoModels = new List<ReferenceNoModel>();
            ReceivablesDisplayDataModel result = new ReceivablesDisplayDataModel();
            result.Current = new ReceivablesDetailsModel();

            if (id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT TOP 1 Id FROM Receivables ORDER BY Id DESC";
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

            result.Data = receivablesModels;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM ReceivablesRefNo WHERE receivablesId=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReferenceNoModel rnm = new ReferenceNoModel();
                                rnm.Id = reader.GetInt32(0);
                                rnm.drNo = reader.GetInt32(1);
                                rnm.invoiceNo = reader.GetInt32(2);
                                rnm.date = reader.GetSqlDateTime(3).Value;
                                rnm.dealer = reader.GetString(4);
                                rnm.drAmount = reader.GetInt32(5);
                                rnm.amount = reader.GetInt32(6);
                                rnm.checkAmount = reader.GetInt32(7);
                                rnm.checkBalance = reader.GetInt32(8);
                                rnm.total = reader.GetInt32(9);

                                referenceNoModels.Add(rnm);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            result.CurrentReferenceNo = referenceNoModels;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Dealers WHERE Id=@did";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@did", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Current.Id = reader.GetInt32(0);
                                result.Current.accountNo = reader.GetInt32(1);
                                result.Current.dealer = reader.GetString(2);
                                result.Current.checkNo = reader.GetInt32(3);
                                result.Current.rtNo = reader.GetInt32(4);
                                result.Current.payToTheOrderOf = reader.GetString(5);
                                result.Current.dateIssued = reader.GetSqlDateTime(6).Value;
                                result.Current.dateDue = reader.GetSqlDateTime(7).Value;
                                result.Current.amount = reader.GetInt32(8);
                                result.Current.status = reader.GetString(9);
                                result.Current.remarks = reader.GetString(10);
                                result.Current.notYetDue = reader.GetBoolean(11);
                                result.Current.cleared = reader.GetBoolean(12);
                                result.Current.onHold = reader.GetBoolean(13);
                                result.Current.others = reader.GetString(14);
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
        
        [HttpPost]
        public IActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Receivables SET accountNo = @an, dealer = @d, bankName = @bn, checkNo = @cn, " +
                                              "rtNo = @rn, payToTheOrderOf = @pttoo, dateIssued = @di, " +
                                              "dateDue = @dd, amount = @a, status = @s, remarks = @r WHERE id=@id";

                        command.Parameters.AddWithValue("@an", Request.Form["accountNo"].ToString());
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
                        command.Parameters.AddWithValue("@id", id);
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

        public IActionResult Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into Receivables (dealer, bankName, checkNo, " +
                                              "rtNo, payToTheOrderOf, dateIssued, dateDue, amount, status, remarks, accountNo) " +
                                              "Values (@d, @bn, @cn, @rn, @pttoo, @di, @dd, @a, @s, @r, @an)";

                        command.Parameters.AddWithValue("@d", "");
                        command.Parameters.AddWithValue("@bn", "");
                        command.Parameters.AddWithValue("@cn", "");
                        command.Parameters.AddWithValue("@rn", "");
                        command.Parameters.AddWithValue("@pttoo", "");
                        command.Parameters.AddWithValue("@di", "");
                        command.Parameters.AddWithValue("@dd", "");
                        command.Parameters.AddWithValue("@a", "");
                        command.Parameters.AddWithValue("@s", "");
                        command.Parameters.AddWithValue("@r", "");
                        command.Parameters.AddWithValue("@an", "");
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