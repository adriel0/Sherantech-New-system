using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Models
{
    public class DataModel
    {
        public int? Id { get; set; }

        [AllowNull]
        public List<SelectListItem> agents { get; set; }
    }
}