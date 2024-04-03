using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using WebApplication1.Models;
using WebApplication1.Models.Purchases;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models.DeliveryReceipts;

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
            result.Current = new PurchasesModel();

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

                    String sql = "SELECT p.id, p.referenceNo, d.DealerBusinessName, p.datePurchased, p.receivedBy, p.closed, p.purchasedFrom FROM Purchases as p INNER JOIN dealers as d on d.id=p.purchasedFrom";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchasesModel pm = new PurchasesModel();
                                pm.Id = reader.GetInt32(0);
                                pm.referenceNo = reader.GetInt32(1);
                                pm.purchasedFrom = reader.GetString(2);
                                pm.datePurchased = reader.GetDateTime(3).Date;
                                pm.receivedBy = reader.GetString(4);
                                pm.closed = reader.GetBoolean(5);
                                pm.purchasedFromNum = reader.GetInt32(6);
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


            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT pi.id, pi.qty, pi.unit, pi.stockNo, pi.unitPrice, pi.amount, pi.remarks, inv.name FROM PurchaceItems as pi INNER JOIN inventory as inv on pi.stockNo=inv.id WHERE pi.purchaseId=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PurchasesItemsModel pim = new PurchasesItemsModel();
                                pim.Id = reader.GetInt32(0);
                                pim.qty = reader.GetInt32(1);
                                pim.unit = reader.GetString(2);
                                pim.stockNo = reader.GetInt32(3);
                                pim.unitPrice = reader.GetInt32(4);
                                pim.amount = reader.GetInt32(5);
                                pim.remarks = reader.GetString(6);
                                pim.stockName = reader.GetString(7);
                                purchasesItemsModels.Add(pim);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            result.CurrentItems = purchasesItemsModels;

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

                                result.Current.Id = reader.GetInt32(0);
                                result.Current.referenceNo = reader.GetInt32(1);
                                result.Current.purchasedFromNum = reader.GetInt32(2);
                                result.Current.datePurchased = reader.GetDateTime(3).Date;
                                result.Current.receivedBy = reader.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }

            




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
                                if (result.Current.purchasedFromNum != null && result.Current.purchasedFromNum.ToString().Equals(d.Value))
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
                        command.CommandText = "UPDATE Purchases SET referenceNo = @rn, purchasedFrom = @pf, datePurchased = @dp, receivedBy = @rb " +
                                              "WHERE id = @id";

                        command.Parameters.AddWithValue("@rn", Request.Form["referenceNo"].ToString());
                        command.Parameters.AddWithValue("@pf", Request.Form["purchasedFrom"].ToString());
                        command.Parameters.AddWithValue("@dp", Request.Form["datePurchased"].ToString());
                        command.Parameters.AddWithValue("@rb", Request.Form["receivedBy"].ToString());
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

        public IActionResult additems(int id)
        {
            PurchasesDisplayDataModel result = new PurchasesDisplayDataModel();
            result.items = new List<SelectListItem>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT i.id,i.name FROM inventory as i ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SelectListItem i = new SelectListItem();
                                i.Value = reader.GetInt32(0).ToString();
                                i.Text = reader.GetString(1);
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
                        command.CommandText = "INSERT Into PurchaceItems (qty, unit, stockNo, unitPrice, remarks, purchaseId) " +
                                              "Values (@q, @u, @s, @up, @r, @id)";

                        command.Parameters.AddWithValue("@q", Request.Form["qty"].ToString());
                        command.Parameters.AddWithValue("@u", Request.Form["unit"].ToString());
                        command.Parameters.AddWithValue("@s", Request.Form["stock"].ToString());
                        command.Parameters.AddWithValue("@up", Request.Form["unitPrice"].ToString());
                        command.Parameters.AddWithValue("@r", Request.Form["remarks"].ToString());
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
                                              "Values (@pf, @dp, @rb, @c, @rn)";

                        command.Parameters.AddWithValue("@pf","1031");
                        command.Parameters.AddWithValue("@dp", "");
                        command.Parameters.AddWithValue("@rb", "");
                        command.Parameters.AddWithValue("@c", "");
                        command.Parameters.AddWithValue("@rn", "");
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
        public IActionResult deleteitems(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "delete from PurchaceItems where id=@id";

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
                        command.CommandText = "delete from PurchaceItems where purchaseId=@id";

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
                        command.CommandText = "delete from Purchases where id=@id";

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