using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Inventory
{
    public class InventoryDisplayDataModel
    {
        [AllowNull]
        public List<InventoryModel>? Data { get; set; }
        [AllowNull]
        public InventoryDetailsModel? CurrentDetails { get; set; }
    }
}
