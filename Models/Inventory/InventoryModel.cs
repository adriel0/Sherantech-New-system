using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Inventory
{
    public class InventoryModel
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
        public BigInteger unitPrice { get; set; }
    }
}
