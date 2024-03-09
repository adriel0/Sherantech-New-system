using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DealersController : Controller
    {
        private readonly ILogger<DealersController> _logger;


        public DealersController(ILogger<DealersController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int id)
        {
            //sql to get top id if no id was sent
            if(id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT Id FROM Dealers ORDER BY DESC LIMIT 1";
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

            List<DealersModel> dealersModels = new List<DealersModel>();
            Console.WriteLine("\nQuery data example:");
            Console.WriteLine("=========================================\n");
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
            //    {
            //        Console.WriteLine("\nQuery data example:");
            //        Console.WriteLine("=========================================\n");

            //        connection.Open();

            //        String sql = "SELECT * FROM Dealers";

            //        using (SqlCommand command = new SqlCommand(sql, connection))
            //        {
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    DealersModel dm = new DealersModel();
            //                    dm.Id = reader.GetInt32(0);
            //                    dm.DealerBusinessName = reader.GetString(1);
            //                    dm.DealerAddress = reader.GetString(2);
            //                    dm.DealerTelNo = reader.GetInt32(3);
            //                    dm.DealerCellNo = reader.GetInt32(4);
            //                    dm.DealerFaxNo = reader.GetInt32(5);
            //                    dm.DealerEmail = reader.GetString(6);
            //                    dm.DealerWebsite = reader.GetString(7);
            //                    dm.DealerBusinessType = reader.GetString(8);
            //                    dm.DealerSecNo = reader.GetInt32(9);
            //                    dm.DealerDateIssued = reader.GetSqlDateTime(10).Value;
            //                    dm.DealerAuthorizationCapital = reader.GetInt32(11);
            //                    dm.DealerSubscribedCapital = reader.GetInt32(12);
            //                    dm.DealerPaidUpCapital = reader.GetInt32(13);
            //                    dm.DTIRegNo = reader.GetInt32(14);
            //                    dm.DTIDateIssued = reader.GetSqlDateTime(15).Value;
            //                    dm.DTIAmtCapital = reader.GetInt32(16);
            //                    dm.DTIPaidUpCapital = reader.GetInt32(17);
            //                    dm.DTITaxAcctNo = reader.GetInt32(18);
            //                    dm.DealerTerms = reader.GetString(19);
            //                    dealersModels.Add(dm);

            //                }
            //            }
            //        }
            //    }
            //}
            //catch (SqlException e)
            //{
            //    Debug.WriteLine(e.ToString());
            //}
            return View();
        }
        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            DealersModel dm = new DealersModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Dealers WHERE Id = " + id;
                    Debug.WriteLine(sql);
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                dm.DealerBusinessName = reader.GetString(1);
                                dm.DealerAddress = reader.GetString(2);
                                dm.DealerTelNo = reader.GetInt32(3);
                                dm.DealerCellNo = reader.GetInt32(4);
                                dm.DealerFaxNo = reader.GetInt32(5);
                                dm.DealerEmail = reader.GetString(6);
                                dm.DealerWebsite = reader.GetString(7);
                                dm.DealerBusinessType = reader.GetString(8);
                                dm.DealerSecNo = reader.GetInt32(9);
                                dm.DealerDateIssued = reader.GetSqlDateTime(10).Value;
                                dm.DealerAuthorizationCapital = reader.GetInt32(11);
                                dm.DealerSubscribedCapital = reader.GetInt32(12);
                                dm.DealerPaidUpCapital = reader.GetInt32(13);
                                dm.DTIRegNo = reader.GetInt32(14);
                                dm.DTIDateIssued = reader.GetSqlDateTime(15).Value;
                                dm.DTIAmtCapital = reader.GetInt32(16);
                                dm.DTIPaidUpCapital = reader.GetInt32(17);
                                dm.DTITaxAcctNo = reader.GetInt32(18);
                                dm.DealerTerms = reader.GetString(19);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(dm);
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
                        command.CommandText = "UPDATE Dealers SET DealerBusinessName = @dbn, DealerAddress = @da, DealerTelNo = @dtn, " +
                                                                 "DealerCellNo = @dcn, DealerFaxNo = @dfn, DealerEmail = @de, " +
                                                                 "DealerWebsite = @dw, DealerBusinessType = @dbt, DealerSecNo = @dsn, " +
                                                                 "DealerDateIssued = @ddi, DealerAuthorizationCapital = @dac, DealerSubscribedCapital = @dsc, " +
                                                                 "DealerPaidUpCapital = @dpuc, DTIRegNo = @dtirn, DTIDateIssued = @dtidi, " +
                                                                 "DTIAmtCapital = @dtiac, DTIPaidUpCapital = @dtipuc, DTITaxAcctNo = @dtitan, " +
                                                                 "DealerTerms = @dt  WHERE Id = @id";

                        command.Parameters.AddWithValue("@dbn", Request.Form["DealerBusinessName"].ToString());
                        command.Parameters.AddWithValue("@da", Request.Form["DealerAddress"].ToString());
                        command.Parameters.AddWithValue("@dtn", Request.Form["DealerTelNo"].ToString());
                        command.Parameters.AddWithValue("@dcn", Request.Form["DealerCellNo"].ToString());
                        //command.Parameters.AddWithValue("@dfn", Request.Form["DealerFaxNo"].ToString());
                        command.Parameters.AddWithValue("@de", Request.Form["DealerEmail"].ToString());
                        command.Parameters.AddWithValue("@dw", Request.Form["DealerWebsite"].ToString());
                        command.Parameters.AddWithValue("@dbt", Request.Form["DealerBusinessType"].ToString());
                        command.Parameters.AddWithValue("@dsn", Request.Form["DealerSecNo"].ToString());
                        command.Parameters.AddWithValue("@ddi", Request.Form["DealerDateIssued"].ToString());
                        command.Parameters.AddWithValue("@dac", Request.Form["DealerAuthorizationCapital"].ToString());
                        command.Parameters.AddWithValue("@dsc", Request.Form["DealerSubscribedCapital"].ToString());
                        command.Parameters.AddWithValue("@dpuc", Request.Form["DealerPaidUpCapital"].ToString());
                        command.Parameters.AddWithValue("@dtirn", Request.Form["DTIRegNo"].ToString());
                        command.Parameters.AddWithValue("@dtidi", Request.Form["DTIDateIssued"].ToString());
                        command.Parameters.AddWithValue("@dtiac", Request.Form["DTIAmtCapital"].ToString());
                        command.Parameters.AddWithValue("@dtipuc", Request.Form["DTIPaidUpCapital"].ToString());
                        command.Parameters.AddWithValue("@dtitan", Request.Form["DTITaxAcctNo"].ToString());
                        command.Parameters.AddWithValue("@dt", Request.Form["DealerTerms"].ToString());
                        command.Parameters.AddWithValue("@id", Request.Form["Id"].ToString());
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
                        command.CommandText = "INSERT Into Dealers (DealerBusinessName, DealerAddress, DealerTelNo, " +
                                                                   "DealerCellNo, DealerFaxNo, DealerEmail, DealerWebsite, DealerBusinessType, DealerSecNo, " +
                                                                   "DealerDateIssued, DealerAuthorizationCapital, DealerSubscribedCapital, DealerPaidUpCapital, " +
                                                                   "DTIRegNo, DTIDateIssued, DTIAmtCapital, DTIPaidUpCapital, DTITaxAcctNo, DealerTerms, " +
                                                                   "Id) Values (@dbn, @da, @dtn, @dcn, @dfn, @de, @dw, @dbt, @dsn, @ddi, @dac, @dsc, @dpuc, @dtirn, @dtidi, @dtiac, @dtipuc, @dtitan, @dt, @id)";

                        command.Parameters.AddWithValue("@dbn", Request.Form["DealerBusinessName"].ToString());
                        command.Parameters.AddWithValue("@da", Request.Form["DealerAddress"].ToString());
                        command.Parameters.AddWithValue("@dtn", Request.Form["DealerTelNo"].ToString());
                        command.Parameters.AddWithValue("@dcn", Request.Form["DealerCellNo"].ToString());
                        command.Parameters.AddWithValue("@dfn", Request.Form["DealerFaxNo"].ToString());
                        command.Parameters.AddWithValue("@de", Request.Form["DealerEmail"].ToString());
                        command.Parameters.AddWithValue("@dw", Request.Form["DealerWebsite"].ToString());
                        command.Parameters.AddWithValue("@dbt", Request.Form["DealerBusinessType"].ToString());
                        command.Parameters.AddWithValue("@dsn", Request.Form["DealerSecNo"].ToString());
                        command.Parameters.AddWithValue("@ddi", Request.Form["DealerDateIssued"].ToString());
                        command.Parameters.AddWithValue("@dac", Request.Form["DealerAuthorizationCapital"].ToString());
                        command.Parameters.AddWithValue("@dsc", Request.Form["DealerSubscribedCapital"].ToString());
                        command.Parameters.AddWithValue("@dpuc", Request.Form["DealerPaidUpCapital"].ToString());
                        command.Parameters.AddWithValue("@dtirn", Request.Form["DTIRegNo"].ToString());
                        command.Parameters.AddWithValue("@dtidi", Request.Form["DTIDateIssued"].ToString());
                        command.Parameters.AddWithValue("@dtiac", Request.Form["DTIAmtCapital"].ToString());
                        command.Parameters.AddWithValue("@dtipuc", Request.Form["DTIPaidUpCapital"].ToString());
                        command.Parameters.AddWithValue("@dtitan", Request.Form["DTITaxAcctNo"].ToString());
                        command.Parameters.AddWithValue("@dt", Request.Form["DealerTerms"].ToString());
                        command.Parameters.AddWithValue("@id", Request.Form["Id"].ToString());
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