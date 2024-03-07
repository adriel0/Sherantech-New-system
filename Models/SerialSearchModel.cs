using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class SerialSearchModel
    {
        [Key]
        public int serialNo { get; set; }
        [AllowNull]
        public int drNo { get; set; }
        [AllowNull]
        public string? stockName { get; set; }
        [AllowNull]
        public string? customer { get; set; }
        [AllowNull]
        public DateTime? dateSold { get; set; }
    }
}
