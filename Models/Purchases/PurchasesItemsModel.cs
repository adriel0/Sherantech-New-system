using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Purchases
{
    public class PurchasesItemsModel
    {
        [Key]
        public int referenceNo { get; set; }
        [AllowNull]
        public int qty { get; set; }
        [AllowNull]
        public string? unit { get; set; }
        [AllowNull]
        public int stockNo { get; set; }
        [AllowNull]
        public int unitPrice { get; set; }
        [AllowNull]
        public int amount { get; set; }
        [AllowNull]
        public string? remarks { get; set; }
    }
}
