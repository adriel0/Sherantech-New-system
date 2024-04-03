using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class DeliveryReceiptsDetailsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public BigInteger invoiceNo { get; set; }
        [AllowNull]
        public int? soldTo { get; set; }
        [AllowNull]
        public int? salesRepresentative { get; set; }
        [AllowNull]
        public string? terms { get; set; }
        [AllowNull]
        public BigInteger POnumber { get; set; }
        [AllowNull]
        public string? others { get; set; }
        [AllowNull]
        public string? address { get; set; }
        [AllowNull]
        public DateTime? dateSold { get; set; }
        [AllowNull]
        public string? remarks { get; set; }
        [AllowNull]
        public bool? autoGenerate { get; set; }
        [AllowNull]
        public string? salesInvoice { get; set; }
        [AllowNull]
        public bool? cancelled { get; set; }
        [AllowNull]
        public bool? closeTransaction { get; set; }
    }
}
