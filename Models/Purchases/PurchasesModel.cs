using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Purchases
{
    public class PurchasesModel
    {
        [Key]
        public int referenceNo { get; set; }
        [AllowNull]
        public string? purchasedFrom { get; set; }
        [AllowNull]
        public DateTime? datePurchased { get; set; }
        [AllowNull]
        public string? receivedBy { get; set; }
        [AllowNull]
        public bool? closed { get; set; }
    }
}
