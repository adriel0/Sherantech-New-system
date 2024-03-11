using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Inventory
{
    public class InventoryModel
    {
        [Key]
        public string? name { get; set; }
        [AllowNull]
        public string? description { get; set; }
        [AllowNull]
        public string? category { get; set; }
        [AllowNull]
        public int unitPrice { get; set; }
    }
}
