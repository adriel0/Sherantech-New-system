using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Inventory
{
    public class InventoryDetailsModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string? name { get; set; }
        [AllowNull]
        public string? description { get; set; }
        [AllowNull]
        public string? category { get; set; }
        [AllowNull]
        public BigInteger? unitPrice { get; set; }
        [AllowNull]
        public BigInteger? unit { get; set; }
        [AllowNull]
        public BigInteger? qtyPerBox { get; set; }
        [AllowNull]
        public int? supplier { get; set; }
        [AllowNull]
        public bool? hasSerial { get; set; }
    }
}
