using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.Models.Dealers;
using WebApplication1.Models.DeliveryReceipts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebApplication1.Controllers
{
    public class DeliveryReceiptsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<DeliveryReceiptsController> _logger;


        public DeliveryReceiptsController(ILogger<DeliveryReceiptsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            List<DeliveryReceiptsModel> deliveryReceiptsModels = new List<DeliveryReceiptsModel>();
            List<DeliveryReceiptsItemsModel> deliveryReceiptsItemsModels = new List<DeliveryReceiptsItemsModel>();
            List<SerialsModel> serialsModels = new List<SerialsModel>();
            List<RemittanceModel> referencesModels = new List<RemittanceModel>();
            DeliveryReceiptsDisplayDataModel result = new DeliveryReceiptsDisplayDataModel();

            if (id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT TOP 1 drNo FROM dr ORDER BY drNo DESC";
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

                    String sql = "SELECT drNo, invoiceNo, soldTo, dateSold, terms FROM dr";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeliveryReceiptsModel d = new DeliveryReceiptsModel();
                                d.drNo = reader.GetInt32(0);
                                d.invoiceNo = reader.GetInt32(1);
                                d.soldTo = reader.GetString(2);
                                d.dateSold = reader.GetSqlDateTime(3).Value;
                                d.terms = reader.GetString(4);


                                deliveryReceiptsModels.Add(d);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.Data = deliveryReceiptsModels;


            result.CurrentDetails = new DeliveryReceiptsDetailsModel();
            result.CurrentDetails.drNo = id;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM dr WHERE drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.CurrentDetails.drNo = reader.GetInt32(0);
                                result.CurrentDetails.invoiceNo = reader.GetInt32(1);
                                result.CurrentDetails.soldTo = reader.GetString(2);
                                result.CurrentDetails.salesRepresentative = reader.GetString(3);
                                result.CurrentDetails.terms = reader.GetString(4);
                                result.CurrentDetails.POnumber = reader.GetInt32(5);
                                result.CurrentDetails.others = reader.GetString(6);
                                result.CurrentDetails.address = reader.GetString(7);
                                result.CurrentDetails.dateSold = reader.GetSqlDateTime(8).Value;
                                result.CurrentDetails.remarks = reader.GetString(9);
                                result.CurrentDetails.closeTransaction = reader.GetBoolean(10);
                                
                            }
                        }
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
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT dri.drno, dri.qty, dri.unit, inv.name, dri.unitPrice, dri.amount, dri.payTo, dri.demo, dri.returned FROM drItems as dri INNER JOIN inventory as inv on dri.article=inv.id WHERE drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeliveryReceiptsItemsModel drim = new DeliveryReceiptsItemsModel();
                                drim.drNo = reader.GetInt32(0);
                                drim.qty = reader.GetInt32(1);
                                drim.unit = reader.GetString(2);
                                drim.article = reader.GetString(3);
                                drim.unitPrice = reader.GetInt32(4);
                                drim.amount = reader.GetInt32(5);
                                drim.payTo = reader.GetString(6);
                                drim.demo = reader.GetBoolean(7);
                                drim.returned = reader.GetBoolean(8);

                                deliveryReceiptsItemsModels.Add(drim);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.CurrentItems = deliveryReceiptsItemsModels;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM drSerials WHERE drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SerialsModel sm = new SerialsModel();
                                sm.drNo = reader.GetInt32(0);
                                sm.serialNo = reader.GetInt32(1);
                                sm.name = reader.GetString(2);
                                sm.warranty = reader.GetInt32(5);
                                sm.free = reader.GetBoolean(6);
                                sm.demo = reader.GetBoolean(7);
                                
                                serialsModels.Add(sm);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.CurrentSerial = serialsModels;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM drRemittance WHERE drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                RemittanceModel rm = new RemittanceModel();
                                rm.drNo = reader.GetInt32(0);
                                rm.checkNo = reader.GetInt32(1);
                                rm.accountNo = reader.GetInt32(2);
                                rm.amount = reader.GetInt32(3);
                                rm.dateIssued = reader.GetSqlDateTime(4).Value;
                                rm.dateDue = reader.GetSqlDateTime(5).Value;
                                rm.status = reader.GetString(6);
                                rm.payToTheOrderOf = reader.GetString(7);
                                rm.bankName = reader.GetString(8);
                                rm.accountName = reader.GetString(9);
                                
                                referencesModels.Add(rm);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.CurrentReferences = referencesModels;


            List < SelectListItem > dl = new List < SelectListItem >();
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


            List<SelectListItem> sl = new List<SelectListItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT id,name FROM salesrep";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem s = new SelectListItem();
                                s.Value = reader.GetInt32(0).ToString();
                                s.Text = reader.GetString(1);
                                sl.Add(s);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.sRep = sl;


            return View(result);
        }
        
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE dr SET invoiceNo = @in, soldTo = @st, dateSold = @ds, terms = @t  WHERE drNo = @dn";

                        command.Parameters.AddWithValue("@in", Request.Form["invoiceNo"].ToString());
                        command.Parameters.AddWithValue("@st", Request.Form["soldTo"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());
                        command.Parameters.AddWithValue("@t", Request.Form["terms"].ToString());
                        command.Parameters.AddWithValue("@dn", id);
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

        public IActionResult addDr()
        {
            return View();
        }
        public IActionResult Addpost()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into dr (invoiceNo, soldTo, dateSold, terms, drNo) Values (@in, @st, @ds, @t, @dn)";

                        command.Parameters.AddWithValue("@in", "");
                        command.Parameters.AddWithValue("@st", "");
                        command.Parameters.AddWithValue("@ds", "");
                        command.Parameters.AddWithValue("@t", "");
                        command.Parameters.AddWithValue("@dn", "");
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