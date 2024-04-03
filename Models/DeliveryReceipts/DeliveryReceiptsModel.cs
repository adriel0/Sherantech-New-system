using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class DeliveryReceiptsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public BigInteger invoiceNo { get; set; }
        [AllowNull]
        public string? soldTo { get; set; }
        [AllowNull]
        public DateTime? dateSold { get; set; }
        [AllowNull]
        public string? terms { get; set; }
    }
}
