using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using WebApplication1.Models.Purchases;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace WebApplication1.Models.Receivables
{
    public class ManageDisplayDataModel
    {
        [AllowNull]
        public List<SelectListItem> drs { get; set; }
        [AllowNull]
        public int id { get; set; }
    }
}
