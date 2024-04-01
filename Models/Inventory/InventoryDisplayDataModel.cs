using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.Inventory
{
    public class InventoryDisplayDataModel
    {
        [AllowNull]
        public List<InventoryModel>? Data { get; set; }
        [AllowNull]
        public InventoryDetailsModel? CurrentDetails { get; set; }
        [AllowNull]
        public List<SelectListItem> dealers { get; set; }
    }
}
