using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class SerialsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public int serialNo { get; set; }
        [AllowNull]
        public string? name { get; set; }
        [AllowNull]
        public string? description { get; set; }
        [AllowNull]
        public string? category { get; set; }
        [AllowNull]
        public int warranty { get; set; }
        [AllowNull]
        public bool? free { get; set; }
        [AllowNull]
        public bool? demo { get; set; }
    }
}
