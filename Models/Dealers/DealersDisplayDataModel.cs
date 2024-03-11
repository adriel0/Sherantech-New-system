using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models.Dealers
{
    public class DealersDisplayDataModel
    {
        [AllowNull]
        public List<DealersModel>? Details { get; set; }
        [AllowNull]
        public List<BankReferenceModel>? CurrentBankRef { get; set; }
        [AllowNull]
        public DealersModel? Current { get; set; }
    }
}
