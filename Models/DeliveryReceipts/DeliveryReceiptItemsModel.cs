using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class DeliveryReceiptsItemsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public BigInteger qty { get; set; }
        [AllowNull]
        public string? unit { get; set; }
        [AllowNull]
        public string? article { get; set; }
        [AllowNull]
        public BigInteger unitPrice { get; set; }
        [AllowNull]
        public BigInteger amount { get; set; }
        [AllowNull]
        public string payTo { get; set; }
        [AllowNull]
        public bool? demo { get; set; }
        [AllowNull]
        public bool? returned { get; set; }
        [AllowNull]
        public int total { get; set; }
        [AllowNull]
        public int? articlenum { get; set; }
    }
}
