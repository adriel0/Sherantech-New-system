using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class DeliveryReceiptsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public int invoiceNo { get; set; }
        [AllowNull]
        public string? soldTo { get; set; }
        [AllowNull]
        public DateTime? dateSold { get; set; }
        [AllowNull]
        public string? terms { get; set; }
    }
}
