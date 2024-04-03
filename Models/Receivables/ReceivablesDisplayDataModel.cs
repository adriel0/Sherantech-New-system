using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using WebApplication1.Models.Purchases;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication1.Models.Receivables
{
    public class ReceivablesDisplayDataModel
    {
        [AllowNull]
        public List<ReceivablesModel>? Data { get; set; }
        [AllowNull]
        public ReceivablesModel? Current { get; set; }
        [AllowNull]
        public List<ReferenceNoModel>? CurrentReferenceNo { get; set; }
        [AllowNull]
        public List<SelectListItem> dealers { get; set; }
        [AllowNull]
        public int id { get; set; }
    }
}
