using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using WebApplication1.Models.Purchases;

namespace WebApplication1.Models.Receivables
{
    public class ReceivablesDisplayDataModel
    {
        [AllowNull]
        public List<ReceivablesModel>? Data { get; set; }
        [AllowNull]
        public ReceivablesDetailsModel? Current { get; set; }
        [AllowNull]
        public List<ReferenceNoModel>? CurrentReferenceNo { get; set; }
    }
}
