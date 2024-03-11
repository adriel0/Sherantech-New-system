using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.DeliveryReceipts
{
    public class DeliveryReceiptsItemsModel
    {
        [Key]
        public int drNo { get; set; }
        [AllowNull]
        public int qty { get; set; }
        [AllowNull]
        public string? unit { get; set; }
        [AllowNull]
        public string? article { get; set; }
        [AllowNull]
        public int unitPrice { get; set; }
        [AllowNull]
        public int amount { get; set; }
        [AllowNull]
        public string? payTo { get; set; }
        [AllowNull]
        public bool? demo { get; set; }
        [AllowNull]
        public bool? returned { get; set; }
        [AllowNull]
        public int total { get; set; }
    }
}
