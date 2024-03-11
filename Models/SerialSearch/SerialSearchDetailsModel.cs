using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.SerialSearch
{
    public class SerialSearchDetailsModel
    {
        [Key]
        public int serialNo { get; set; }
        [AllowNull]
        public string? stockName { get; set; }
        [AllowNull]
        public string? customer { get; set; }
        [AllowNull]
        public int drNumber { get; set; }
        [AllowNull]
        public DateTime? dateSold { get; set; }
        [AllowNull]
        public string? terms { get; set; }
        [AllowNull]
        public bool? free { get; set; }
        [AllowNull]
        public bool? demo { get; set; }
        [AllowNull]
        public int warrantyPeriod { get; set; }
        [AllowNull]
        public DateTime? endOfWarranty { get; set; }
        [AllowNull]
        public DateTime? extendedWarranty { get; set; }
        [AllowNull]
        public DateTime? endOfExtendedWarranty { get; set; }
        [AllowNull]
        public string? category { get; set; }
        [AllowNull]
        public string? remarks { get; set; }
    }
}
