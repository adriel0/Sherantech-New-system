using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Reports
{
    public class ReportsDisplayDataModel
    {
        [AllowNull]
        public List<ReportsModel> report { get; set; }
    }
}
