using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models.Purchases
{
    public class PurchasesDisplayDataModel
    {
        [AllowNull]
        public List<PurchasesModel>? Details { get; set; }
        [AllowNull]
        public List<PurchasesItemsModel>? CurrentItems { get; set; }
        [AllowNull]
        public PurchasesModel? Current { get; set; }
        [AllowNull]
        public List<SelectListItem> dealers { get; set; }
        [AllowNull]
        public List<SelectListItem> items { get; set; }
        [AllowNull]
        public int id { get; set; }
    }
}
