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
            SqlConnection disp = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]);
            Debug.WriteLine(disp.ToString());
            List<DealersModel> dealersModels = new List<DealersModel>();
            List<BankReferenceModel> bankReferenceModel = new List<BankReferenceModel>();
            List<AgentsModel> agentsModel = new List<AgentsModel>();
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
                                dm.DealerDateIssued = reader.GetDateTime(10);
                                dm.DealerAuthorizationCapital = reader.GetInt64(11);
                                dm.DealerSubscribedCapital = reader.GetInt64(12);
                                dm.DealerPaidUpCapital = reader.GetInt64(13);
                                dm.DTIRegNo = reader.GetInt64(14);
                                dm.DTIDateIssued = reader.GetDateTime(15);
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

                    String sql = "SELECT * FROM Dealers WHERE Id=@did";

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
                                result.Current.DealerDateIssued = reader.GetDateTime(10);
                                result.Current.DealerAuthorizationCapital = reader.GetInt64(11);
                                result.Current.DealerSubscribedCapital = reader.GetInt64(12);
                                result.Current.DealerPaidUpCapital = reader.GetInt64(13);
                                result.Current.DTIRegNo = reader.GetInt64(14);
                                result.Current.DTIDateIssued = reader.GetDateTime(15);
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

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM DealerBankRef WHERE dealerId=@did";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@did", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BankReferenceModel brm = new BankReferenceModel();
                                brm.Id = reader.GetInt32(0);
                                brm.accountNo = reader.GetString(1);
                                brm.accountName = reader.GetString(2);
                                brm.type = reader.GetString(3);
                                brm.bank = reader.GetString(4);
                                brm.dealerId = reader.GetInt32(5);
                                bankReferenceModel.Add(brm);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.CurrentBankRef = bankReferenceModel;

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT * FROM DealerAgents WHERE dealerId=@did";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@did", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AgentsModel am = new AgentsModel();
                                am.Id = reader.GetInt32(0);
                                am.lastName = reader.GetString(1);
                                am.firstName = reader.GetString(2);
                                am.isDefault = reader.GetString(3);
                                am.dealerId = reader.GetInt32(4);
                                agentsModel.Add(am);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            result.CurrentAgents = agentsModel;
            return View(result);
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
                        command.CommandText = "UPDATE Dealers SET DealerBusinessName = @dbn, DealerAddress = @da, DealerTelNo = @dtn, " +
                                                                 "DealerCellNo = @dcn, DealerFaxNo = @dfn, DealerEmail = @de, " +
                                                                 "DealerWebsite = @dw, DealerBusinessType = @dbt, DealerSecNo = @dsn, " +
                                                                 "DealerDateIssued = @ddi, DealerAuthorizationCapital = @dac, DealerSubscribedCapital = @dsc, " +
                                                                 "DealerPaidUpCapital = @dpuc, DTIRegNo = @dtirn, DTIDateIssued = @dtidi, " +
                                                                 "DTIAmtCapital = @dtiac, DTIPaidUpCapital = @dtipuc, DTITaxAcctNo = @dtitan, " +
                                                                 "DealerTerms = @dt  WHERE Id = @did";

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
                        command.Parameters.AddWithValue("@did", id);
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

        //CRUD for bankref
        public IActionResult addBankRef(int id)
        {
            DataModel dm = new DataModel();
            dm.Id = id;
            return View(dm);
        }
        public IActionResult addbankrefpost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into DealerBankRef (accountNo, accountName, type, bank, dealerId) " +
                                                                   " Values (@anum, @an, @bt, @bn, @did)";

                        command.Parameters.AddWithValue("@anum", Request.Form["AccountNo"].ToString());
                        command.Parameters.AddWithValue("@an", Request.Form["AccountName"].ToString());
                        command.Parameters.AddWithValue("@bt", Request.Form["Type"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["BankName"].ToString());
                        command.Parameters.AddWithValue("@did", id);
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
        public IActionResult EditBankRef(int id)
        {
            BankReferenceModel brm = new BankReferenceModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    connection.Open();

                    String sql = "SELECT * FROM DealerBankRef WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                brm.Id = reader.GetInt32(0);
                                brm.accountNo = reader.GetString(1);
                                brm.accountName = reader.GetString(2);
                                brm.type = reader.GetString(3);
                                brm.bank = reader.GetString(4);
                                brm.dealerId = reader.GetInt32(5);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(brm);
        }

        [HttpPost]
        public IActionResult EditBankRefPost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE DealerBankRef SET accountNo = @anum, accountName = @an, type = @bt, " +
                                                                 "bank = @bn WHERE Id = @id";

                        command.Parameters.AddWithValue("@anum", Request.Form["AccountNo"].ToString());
                        command.Parameters.AddWithValue("@an", Request.Form["AccountName"].ToString());
                        command.Parameters.AddWithValue("@bt", Request.Form["Type"].ToString());
                        command.Parameters.AddWithValue("@bn", Request.Form["BankName"].ToString());
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

        public IActionResult DeleteBankRef(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM DealerBankRef WHERE Id=@id";
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

        //CRUD for agents
        public IActionResult addAgents(int id)
        {
            DataModel dm = new DataModel();
            dm.Id = id;
            return View(dm);
        }
        public IActionResult addAgentspost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "INSERT Into DealerAgents (firstName, lastName, isDefault, dealerId) " +
                                                                   " Values (@fn, @ln, @isd, @did)";

                        command.Parameters.AddWithValue("@fn", Request.Form["firstname"].ToString());
                        command.Parameters.AddWithValue("@ln", Request.Form["lastname"].ToString());
                        command.Parameters.AddWithValue("@isd", Request.Form["isDefault"].ToString());
                        command.Parameters.AddWithValue("@did", id);
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
        public IActionResult EditAgents(int id)
        {
            AgentsModel am = new AgentsModel();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    connection.Open();

                    String sql = "SELECT * FROM DealerAgents WHERE Id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                am.Id = reader.GetInt32(0);
                                am.lastName = reader.GetString(1);
                                am.firstName = reader.GetString(2);
                                am.isDefault = reader.GetString(3);
                                am.dealerId = reader.GetInt32(4);
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.ToString());
            }
            return View(am);
        }

        [HttpPost]
        public IActionResult EditAgentsPost(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE DealerAgents SET firstName = @fn, lastName = @ln, isDefault = @isd " +
                                                                 "WHERE Id = @id";

                        command.Parameters.AddWithValue("@fn", Request.Form["firstname"].ToString());
                        command.Parameters.AddWithValue("@ln", Request.Form["lastname"].ToString());
                        command.Parameters.AddWithValue("@isd", Request.Form["isDefault"].ToString());
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

        public IActionResult DeleteAgents(int id)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "DELETE FROM DealerAgents WHERE Id=@id";
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


        public IActionResult Add()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText ="INSERT Into Dealers (DealerBusinessName, DealerAddress, DealerTelNo, " +
                                                                   "DealerCellNo, DealerFaxNo, DealerEmail, DealerWebsite, DealerBusinessType, DealerSecNo, " +
                                                                   "DealerDateIssued, DealerAuthorizationCapital, DealerSubscribedCapital, DealerPaidUpCapital, " +
                                                                   "DTIRegNo, DTIDateIssued, DTIAmtCapital, DTIPaidUpCapital, DTITaxAcctNo, DealerTerms " +
                                                                   ") Values (@dbn, @da, @dtn, @dcn, @dfn, @de, @dw, @dbt, @dsn, @ddi, @dac, @dsc, @dpuc, @dtirn, @dtidi, @dtiac, @dtipuc, @dtitan, @dt)";

                        command.Parameters.AddWithValue("@dbn", "");
                        command.Parameters.AddWithValue("@da", "");
                        command.Parameters.AddWithValue("@dtn", "");
                        command.Parameters.AddWithValue("@dcn", "");
                        command.Parameters.AddWithValue("@dfn", "");
                        command.Parameters.AddWithValue("@de", "");
                        command.Parameters.AddWithValue("@dw", "");
                        command.Parameters.AddWithValue("@dbt", "");
                        command.Parameters.AddWithValue("@dsn", "");
                        command.Parameters.AddWithValue("@ddi", "");
                        command.Parameters.AddWithValue("@dac", "");
                        command.Parameters.AddWithValue("@dsc", "");
                        command.Parameters.AddWithValue("@dpuc", "");
                        command.Parameters.AddWithValue("@dtirn", "");
                        command.Parameters.AddWithValue("@dtidi", "");
                        command.Parameters.AddWithValue("@dtiac", "");
                        command.Parameters.AddWithValue("@dtipuc", "");
                        command.Parameters.AddWithValue("@dtitan", "");
                        command.Parameters.AddWithValue("@dt", "");
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
                        command.CommandText = "DELETE FROM DealerBankRef WHERE dealerId=@did";

                        command.Parameters.AddWithValue("@did", id);
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
                        command.CommandText = "DELETE FROM DealerAgents WHERE dealerId=@did";

                        command.Parameters.AddWithValue("@did", id);
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
                        command.CommandText = "DELETE FROM Dealers WHERE id=@did";

                        command.Parameters.AddWithValue("@did", id);
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