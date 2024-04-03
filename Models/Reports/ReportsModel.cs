using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Reports
{
    public class ReportsModel
    {
        [AllowNull]
        public DateTime dateSold { get; set; }
        [AllowNull]
        public int? drNo { get; set; }
        [AllowNull]
        public BigInteger? qty { get; set; }
        [AllowNull]
        public string? iName { get; set; }
        [AllowNull]
        public BigInteger? unitPrice { get; set; }
        [AllowNull]
        public string? dName { get; set; }
        [AllowNull]
        public BigInteger? amount { get; set; }
    }
}
