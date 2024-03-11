using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using System.Reflection.PortableExecutable;
using WebApplication1.Models;
using WebApplication1.Models.Dealers;

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

            List<DealersModel> dealersModels = new List<DealersModel>();
            DealersDisplayDataModel result = new DealersDisplayDataModel();
            int nid=0;
            //sql to get top id if no id was sent
            if (id == 0)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                    {
                        connection.Open();
                        String sql = "SELECT TOP 1 Id FROM Dealers ORDER BY Id DESC";
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

                Debug.WriteLine($"Dealers: {nid}");
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

                    String sql = "SELECT * FROM Dealers";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DealersModel dm = new DealersModel();
                                dm.Id = reader.GetInt32(0);
                                dm.DealerBusinessName = reader.GetString(1);
                                dm.DealerAddress = reader.GetString(2);
                                dm.DealerTelNo = reader.GetString(3);
                                dm.DealerCellNo = reader.GetString(4);
                                dm.DealerFaxNo = reader.GetString(5);
                                dm.DealerEmail = reader.GetString(6);
                                dm.DealerWebsite = reader.GetString(7);
                                dm.DealerBusinessType = reader.GetString(8);
                                dm.DealerSecNo = reader.GetString(9);
                                dm.DealerDateIssued = reader.GetSqlDateTime(10).Value;
                                dm.DealerAuthorizationCapital = reader.GetInt64(11);
                                dm.DealerSubscribedCapital = reader.GetInt64(12);
                                dm.DealerPaidUpCapital = reader.GetInt64(13);
                                dm.DTIRegNo = reader.GetInt64(14);
                                dm.DTIDateIssued = reader.GetSqlDateTime(15).Value;
                                dm.DTIAmtCapital = reader.GetInt64(16);
                                dm.DTIPaidUpCapital = reader.GetInt64(17);
                                dm.DTITaxAcctNo = reader.GetInt64(18);
                                dm.DealerTerms = reader.GetString(19);
                                dealersModels.Add(dm);

                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.Details= dealersModels;


            result.Current = new DealersModel();
            result.Current.Id = id;
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM Dealers WHERE id=@did";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@did", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Current.DealerBusinessName = reader.GetString(1);
                                result.Current.DealerAddress = reader.GetString(2);
                                result.Current.DealerTelNo = reader.GetString(3);
                                result.Current.DealerCellNo = reader.GetString(4);
                                result.Current.DealerFaxNo = reader.GetString(5);
                                result.Current.DealerEmail = reader.GetString(6);
                                result.Current.DealerWebsite = reader.GetString(7);
                                result.Current.DealerBusinessType = reader.GetString(8);
                                result.Current.DealerSecNo = reader.GetString(9);
                                result.Current.DealerDateIssued = reader.GetSqlDateTime(10).Value;
                                result.Current.DealerAuthorizationCapital = reader.GetInt64(11);
                                result.Current.DealerSubscribedCapital = reader.GetInt64(12);
                                result.Current.DealerPaidUpCapital = reader.GetInt64(13);
                                result.Current.DTIRegNo = reader.GetInt64(14);
                                result.Current.DTIDateIssued = reader.GetSqlDateTime(15).Value;
                                result.Current.DTIAmtCapital = reader.GetInt64(16);
                                result.Current.DTIPaidUpCapital = reader.GetInt64(17);
                                result.Current.DTITaxAcctNo = reader.GetInt64(18);
                                result.Current.DealerTerms = reader.GetString(19);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(result);
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
                                dm.DealerTelNo = reader.GetString(3);
                                dm.DealerCellNo = reader.GetString(4);
                                dm.DealerFaxNo = reader.GetString(5);
                                dm.DealerEmail = reader.GetString(6);
                                dm.DealerWebsite = reader.GetString(7);
                                dm.DealerBusinessType = reader.GetString(8);
                                dm.DealerSecNo = reader.GetString(9);
                                dm.DealerDateIssued = reader.GetSqlDateTime(10).Value;
                                dm.DealerAuthorizationCapital = reader.GetInt64(11);
                                dm.DealerSubscribedCapital = reader.GetInt64(12);
                                dm.DealerPaidUpCapital = reader.GetInt64(13);
                                dm.DTIRegNo = reader.GetInt64(14);
                                dm.DTIDateIssued = reader.GetSqlDateTime(15).Value;
                                dm.DTIAmtCapital = reader.GetInt64(16);
                                dm.DTIPaidUpCapital = reader.GetInt64(17);
                                dm.DTITaxAcctNo = reader.GetInt64(18);
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