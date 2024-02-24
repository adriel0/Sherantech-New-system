using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
        [DisallowNull]
        public int DealerAccountId { get; set;}

        [AllowNull]
        public int? DealerTelNo { get; set; }
        [AllowNull]
        public int? DealerCellNo { get; set; }
        [AllowNull]
        public int? DealerFaxNo { get; set; }
        [AllowNull]
        public string? DealerEmail { get; set; }
        [AllowNull]
        public string? DealerWebsite { get; set; }
        [AllowNull]
        public string? DealerBusinessType { get; set; }
        [AllowNull]
        public int? DealerSecNo { get; set; }
        [AllowNull]
        public string? DealerDateIssued { get; set; }
        [AllowNull]
        public int? DealerAuthorizationCapital { get; set; }
        [AllowNull]
        public int? DealerSubscribedCapital { get; set; }
        [AllowNull]
        public int? DealerPaidUpCapital { get; set; }
        [AllowNull]
        public int? DTIRegNo { get; set; }
        [AllowNull]
        public string? DTIDateIssued { get; set; }
        [AllowNull]
        public int? DTIAmtCapital { get; set; }
        [AllowNull]
        public int? DTIPaidUpCapital { get; set; }
        [AllowNull]
        public int? DTITaxAcctNo { get; set; }
        [AllowNull]
        public string? DealerTerms { get; set; }

    }
}
