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

                    String sql = "SELECT drNo, invoiceNo, Dealers.DealerBusinessName, dateSold, terms FROM dr INNER JOIN Dealers on dr.soldTo = Dealers.id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeliveryReceiptsModel d = new DeliveryReceiptsModel();
                                d.drNo = reader.GetInt32(0);
                                d.invoiceNo = reader.GetInt64(1);
                                d.soldTo = reader.GetString(2);
                                d.dateSold = reader.GetDateTime(3);
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
                                result.CurrentDetails.invoiceNo = reader.GetInt64(1);
                                result.CurrentDetails.soldTo = reader.GetInt32(2);
                                result.CurrentDetails.salesRepresentative = reader.GetInt32(3);
                                result.CurrentDetails.terms = reader.GetString(4);
                                result.CurrentDetails.POnumber = reader.GetInt64(5);
                                result.CurrentDetails.address = reader.GetString(7);
                                result.CurrentDetails.dateSold = reader.GetDateTime(8);
                                result.CurrentDetails.remarks = reader.GetString(9);
                                
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

                    String sql = "SELECT dri.drno, dri.qty, dri.unit, inv.name, dri.unitPrice, dri.amount, dri.payTo, dri.article, dri.id FROM drItems as dri INNER JOIN inventory as inv on dri.article=inv.id WHERE drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DeliveryReceiptsItemsModel drim = new DeliveryReceiptsItemsModel();
                                drim.drNo = reader.GetInt32(0);
                                drim.qty = reader.GetInt64(1);
                                drim.unit = reader.GetString(2);
                                drim.article = reader.GetString(3);
                                drim.unitPrice = reader.GetInt64(4);
                                drim.amount = reader.GetInt64(5);
                                drim.payTo = reader.GetString(6);
                                drim.articlenum = reader.GetInt32(7);
                                drim.id = reader.GetInt32(8);

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

            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
            //    {
            //        Console.WriteLine("\nQuery data example:");
            //        Console.WriteLine("=========================================\n");

            //        connection.Open();

            //        String sql = "SELECT drs.drNo, drs.serialNo, inv.name, drs.warranty, drs.free, drs.demo, drs.item FROM drSerials as drs INNER JOIN inventory as inv on inv.id = drSerials.item WHERE drNo=@id";

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            command.Parameters.AddWithValue("@id", id);
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    SerialsModel sm = new SerialsModel();
            //                    sm.drNo = reader.GetInt32(0);
            //                    sm.serialNo = reader.GetInt32(1);
            //                    sm.name = reader.GetString(2);
            //                    sm.warranty = reader.GetString(3);
            //                    sm.free = reader.GetBoolean(4);
            //                    sm.demo = reader.GetBoolean(5);
            //                    sm.namenum = reader.GetInt32(6);

            //                    serialsModels.Add(sm);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SqlException e)
            //{
            //    Debug.WriteLine(e.ToString());
            //}
            //result.CurrentSerial = serialsModels;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT rn.drNo,r.checkNo,r.accountNo,rn.amount,r.dateIssued,r.dateDue,r.status,r.payToTheOrderOf,r.bankName FROM ReceivablesRefNo as rn INNER JOIN receivables as r on r.id=rn.receivablesId WHERE rn.drNo=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                RemittanceModel rm = new RemittanceModel();
                                rm.drNo = reader.GetInt32(0);
                                rm.checkNo = reader.GetString(1);
                                rm.accountNo = reader.GetInt32(2);
                                rm.amount = reader.GetInt32(3);
                                rm.dateIssued = reader.GetDateTime(4);
                                rm.dateDue = reader.GetDateTime(5);
                                rm.status = reader.GetString(6);
                                rm.payToTheOrderOf = reader.GetString(7);
                                rm.bankName = reader.GetString(8);
                                
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
                                if (result.CurrentDetails.soldTo != null && result.CurrentDetails.soldTo.ToString().Equals(d.Value))
                                {
                                    d.Selected = true;
                                }
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
                                if (result.CurrentDetails.salesRepresentative != null && result.CurrentDetails.salesRepresentative.ToString().Equals(s.Value))
                                {
                                    s.Selected = true;
                                }
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


      


        public IActionResult Edit(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE dr SET invoiceNo = @in, soldTo = @st, salesRepresentative = @sr, terms = @t, PONumber = @pon, address=@a, dateSold=@ds, remarks=@r WHERE drNo = @dn";

                        command.Parameters.AddWithValue("@in", Request.Form["invoiceNo"].ToString());
                        command.Parameters.AddWithValue("@st", Request.Form["soldTo"].ToString());
                        command.Parameters.AddWithValue("@sr", Request.Form["sRep"].ToString());
                        command.Parameters.AddWithValue("@t", Request.Form["terms"].ToString());
                        command.Parameters.AddWithValue("@pon", Request.Form["PONum"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["address"].ToString());
                        command.Parameters.AddWithValue("@ds", Request.Form["dateSold"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());
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
        public IActionResult additems(int id)
        {
            DeliveryReceiptsDisplayDataModel result = new DeliveryReceiptsDisplayDataModel();
            result.items = new List<SelectListItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT i.id,i.name,sum(pi.qty),isnull(sum(dri.qty), 0) FROM inventory as i " +
                        "INNER JOIN PurchaceItems as pi on pi.stockNo = i.id " +
                        "FULL JOIN drItems as dri on dri.article = i.id " +
                        "group by i.id,i.name";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem i = new SelectListItem();
                                i.Value = reader.GetInt32(0).ToString();
                                i.Text = reader.GetString(1) +" : "+(reader.GetInt32(2)- reader.GetInt64(3)).ToString();
                                result.items.Add(i);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            result.id = id;
            return View(result);
        }
        public IActionResult additemspost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into drItems (qty, unit, article, unitprice, payto, drNo) Values (@q, @u, @a, @up, @pt, @id)";

                        command.Parameters.AddWithValue("@q", Request.Form["qty"].ToString());
                        command.Parameters.AddWithValue("@u", Request.Form["unit"].ToString());
                        command.Parameters.AddWithValue("@a", Request.Form["article"].ToString());
                        command.Parameters.AddWithValue("@up", Request.Form["unitprice"].ToString());
                        command.Parameters.AddWithValue("@pt", Request.Form["payto"].ToString());
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
        public IActionResult deleteitem(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "delete from drItems where id=@id";

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
                        command.CommandText = "delete from drItems where drNo=@id";

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
                        command.CommandText = "delete from dr where drNo=@id";

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

        public IActionResult add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into dr (invoiceNo, soldTo, salesRepresentative, dateSold,PONumber, terms, address, remarks) Values (@in, @st, @sr, @ds,@pon, @t, @a, @r)";

                        command.Parameters.AddWithValue("@in", "");
                        command.Parameters.AddWithValue("@st", "1031");
                        command.Parameters.AddWithValue("@sr", "1");
                        command.Parameters.AddWithValue("@ds", "");
                        command.Parameters.AddWithValue("@pon", "");
                        command.Parameters.AddWithValue("@t", "");
                        command.Parameters.AddWithValue("@a", "");
                        command.Parameters.AddWithValue("@r", "");
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