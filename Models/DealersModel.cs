using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace WebApplication1.Models
{
    public class DealersModel
    {
        [Key]
        public int Id { get; set;}
        [AllowNull]
        public string? DealerBusinessName { get; set;}
        [AllowNull]
        public string? DealerAddress { get; set;}

        [AllowNull]
        public string? DealerTelNo { get; set; }
        [AllowNull]
        public string? DealerCellNo { get; set; }
        [AllowNull]
        public string? DealerFaxNo { get; set; }
        [AllowNull]
        public string? DealerEmail { get; set; }
        [AllowNull]
        public string? DealerWebsite { get; set; }
        [AllowNull]
        public string? DealerBusinessType { get; set; }
        [AllowNull]
        public string? DealerSecNo { get; set; }
        [AllowNull]
        public DateTime? DealerDateIssued { get; set; }
        [AllowNull]
        public BigInteger? DealerAuthorizationCapital { get; set; }
        [AllowNull]
        public BigInteger? DealerSubscribedCapital { get; set; }
        [AllowNull]
        public BigInteger? DealerPaidUpCapital { get; set; }
        [AllowNull]
        public BigInteger? DTIRegNo { get; set; }
        [AllowNull]
        public DateTime? DTIDateIssued { get; set; }
        [AllowNull]
        public BigInteger? DTIAmtCapital { get; set; }
        [AllowNull]
        public BigInteger? DTIPaidUpCapital { get; set; }
        [AllowNull]
        public BigInteger? DTITaxAcctNo { get; set; }
        [AllowNull]
        public string? DealerTerms { get; set; }

    }
}
