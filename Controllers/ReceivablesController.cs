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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

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
            result.Current = new ReceivablesModel();

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

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT r.accountNo, d.DealerBusinessName, r.bankName, r.checkNo, r.rtNo, r.payToTheorderOf, r.dateIssued, r.dateDue, r.amount, r.status, r.remarks, r.id, r.dealers FROM Receivables as r " +
                        "INNER JOIN Dealers as d on d.id = r.dealers " +
                        "WHERE r.id = @id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Current.accountNo = reader.GetInt32(0);
                                result.Current.dealer = reader.GetString(1);
                                result.Current.bankName = reader.GetString(2);
                                result.Current.checkNo = reader.GetString(3);
                                result.Current.rtNo = reader.GetInt32(4);
                                result.Current.payToTheOrderOf = reader.GetString(5);
                                result.Current.dateIssued = reader.GetDateTime(6);
                                result.Current.dateDue = reader.GetDateTime(7);
                                result.Current.amount = reader.GetInt32(8);
                                result.Current.status = reader.GetString(9);
                                result.Current.remarks = reader.GetString(10);
                                result.Current.Id = reader.GetInt32(11);
                                result.Current.dealerNum = reader.GetInt32(12);


                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
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

                    String sql = "SELECT r.accountNo, d.DealerBusinessName, r.bankName, r.checkNo, r.rtNo, r.payToTheorderOf, r.dateIssued, r.dateDue, r.amount, r.status, r.remarks, r.id FROM Receivables as r " +
                        "INNER JOIN Dealers as d on d.id = r.dealers ";

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
                                rm.checkNo = reader.GetString(3);
                                rm.rtNo = reader.GetInt32(4);
                                rm.payToTheOrderOf = reader.GetString(5);
                                rm.dateIssued = reader.GetDateTime(6);
                                rm.dateDue = reader.GetDateTime(7);
                                rm.amount = reader.GetInt32(8);
                                rm.status = reader.GetString(9);
                                rm.remarks = reader.GetString(10);
                                rm.Id = reader.GetInt32(11);

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

                    String sql = "SELECT rn.id,rn.drNo,rn.amount FROM ReceivablesRefNo as rn " +
                        "WHERE receivablesId=@id";

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
                                rnm.amount = reader.GetInt32(2);

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
        
        public IActionResult manage(int id,int dealerid)
        {
            ManageDisplayDataModel result = new ManageDisplayDataModel();
            List<SelectListItem> drp = new List<SelectListItem>();
            result.id = id;
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT dr.drNo,SUM(dri.amount) as amount FROM dr INNER JOIN drItems as dri on dri.drNo = dr.drNo WHERE soldTo=@did GROUP BY dr.drNo";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@did", dealerid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem d = new SelectListItem();
                                d.Value = reader.GetInt32(0).ToString();
                                d.Text = reader.GetInt32(0).ToString() +" : "+ reader.GetInt64(1).ToString();
                                drp.Add(d);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.drs = drp;

            return View(result);
        }

        public IActionResult managepost(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into ReceivablesRefNo (drno, amount, receivablesId) " +
                                              "Values (@dr, @a, @rid)";

                        command.Parameters.AddWithValue("@dr", Request.Form["dr"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["amount"].ToString());
                        command.Parameters.AddWithValue("@rid", id);
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

        [HttpPost]
        public IActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE Receivables SET accountNo = @an, bankName = @bn, checkNo = @cn, " +
                                              "rtNo = @rn, payToTheOrderOf = @pttoo, dateIssued = @di, " +
                                              "dateDue = @dd, amount = @a, status = @s, remarks = @r WHERE id=@id";

                        command.Parameters.AddWithValue("@an", Request.Form["accountNo"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["bank"].ToString());
                        command.Parameters.AddWithValue("@cn", Request.Form["checkNo"].ToString());
                        command.Parameters.AddWithValue("@rn", Request.Form["rtNo"].ToString());
                        command.Parameters.AddWithValue("@pttoo", Request.Form["payToTheOrderOf"].ToString());
                        command.Parameters.AddWithValue("@di", Request.Form["dateIssued"].ToString());
                        command.Parameters.AddWithValue("@dd", Request.Form["dateDue"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["amount"].ToString());
                        command.Parameters.AddWithValue("@s", Request.Form["status"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());
                        command.Parameters.AddWithValue("@id", id);
                        Console.WriteLine(Request.Form["bank"].ToString());
                        Console.WriteLine(Request.Form["bank"].ToString());
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

        public IActionResult Add(int id)
        {


            ReceivablesDisplayDataModel result = new ReceivablesDisplayDataModel();

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

            

            result.id = id;
            return View(result);
        }

        public IActionResult AddPost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into Receivables (dealers, bankName, checkNo, " +
                                              "rtNo, payToTheOrderOf, dateIssued, dateDue, amount, status, remarks, accountNo ) " +
                                              "Values (@d, @bn, @cn, @rn, @pttoo, @di, @dd, @a, @s, @r, @an)";

                        command.Parameters.AddWithValue("@d", Request.Form["dealer"].ToString());
                        command.Parameters.AddWithValue("@an", Request.Form["accountNo"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["bank"].ToString());
                        command.Parameters.AddWithValue("@cn", Request.Form["checkNo"].ToString());
                        command.Parameters.AddWithValue("@rn", Request.Form["rtNo"].ToString());
                        command.Parameters.AddWithValue("@pttoo", Request.Form["payToTheOrderOf"].ToString());
                        command.Parameters.AddWithValue("@di", Request.Form["dateIssued"].ToString());
                        command.Parameters.AddWithValue("@dd", Request.Form["dateDue"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["amount"].ToString());
                        command.Parameters.AddWithValue("@s", Request.Form["status"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());
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
        public IActionResult deleteref(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "delete from ReceivablesRefNo where id=@id";

                        command.Parameters.AddWithValue("@id", id);
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
        public IActionResult delete(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "delete from ReceivablesRefNo where receivablesId=@id";

                        command.Parameters.AddWithValue("@id", id);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "delete from receivables where id=@id";

                        command.Parameters.AddWithValue("@id", id);
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}