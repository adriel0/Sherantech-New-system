using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Inventory
{
    public class InventoryDetailsModel
    {
        [Key]
        public string? name { get; set; }
        [AllowNull]
        public string? description { get; set; }
        [AllowNull]
        public string? category { get; set; }
        [AllowNull]
        public int unitPrice { get; set; }
        [AllowNull]
        public string? unit { get; set; }
        [AllowNull]
        public int qtyPerBox { get; set; }
        [AllowNull]
        public int purchasePrice { get; set; }
        [AllowNull]
        public int volumePrice { get; set; }
        [AllowNull]
        public int volumeQuantity { get; set; }
        [AllowNull]
        public int warrantyPeriod { get; set; }
        [AllowNull]
        public int extendedWarranty { get; set; }
        [AllowNull]
        public string? supplier { get; set; }
        [AllowNull]
        public int minLvl { get; set; }
        [AllowNull]
        public int maxLvl { get; set; }
        [AllowNull]
        public int markUp { get; set; }
        [AllowNull]
        public bool? hasSerial { get; set; }
    }
}
