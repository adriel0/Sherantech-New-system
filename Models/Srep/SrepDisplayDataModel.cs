using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Srep
{
    public class SrepDisplayDataModel
    {
        [AllowNull]
        public List<SrepModel>? CurrentSrep { get; set; }
    }
}
