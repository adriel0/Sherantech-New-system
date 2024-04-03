using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net;
using WebApplication1.Models;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using WebApplication1.Models.Reports;
using System.Runtime.Intrinsics.Arm;


namespace WebApplication1.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ILogger<ReportsController> _logger;


        public ReportsController(ILogger<ReportsController> logger)
        {
            _logger = logger;
        }

        public IActionResult index()
        {
            ReportsDisplayDataModel result = new ReportsDisplayDataModel();
            result.report = new List<ReportsModel>();
            try
            {
                using (SqlConnection connection = new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["defaultConnection"]))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");

                    connection.Open();

                    String sql = "SELECT  dr.datesold, dr.drNo, dri.qty, i.name, dri.unitprice, d.DealerBusinessName, dri.amount from dritems as dri " +
                        "INNER JOIN dr ON dr.drNo = dri.drNo " +
                        "INNER JOIN inventory as i ON dri.article = i.id " +
                        "INNER JOIN dealers as d ON dr.soldTo = d.Id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ReportsModel r = new ReportsModel();
                                r.dateSold = reader.GetDateTime(0);
                                r.drNo = reader.GetInt32(1);
                                r.qty = reader.GetInt64(2);
                                r.iName = reader.GetString(3);
                                r.unitPrice = reader.GetInt64(4);
                                r.dName = reader.GetString(5);
                                r.amount = reader.GetInt64(6);
                                result.report.Add(r);

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

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}