using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Receivables
{
    public class ReceivablesDetailsModel
    {
        [Key]
        public int accountNo { get; set; }
        [AllowNull]
        public string? dealer { get; set; }
        [AllowNull]
        public int checkNo { get; set; }
        [AllowNull]
        public int rtNo { get; set; }
        [AllowNull]
        public string? payToTheOrderOf { get; set; }
        [AllowNull]
        public DateTime? dateIssued { get; set; }
        [AllowNull]
        public DateTime? dateDue { get; set; }
        [AllowNull]
        public int amount { get; set; }
        [AllowNull]
        public string? status { get; set; }
        [AllowNull]
        public string? remarks { get; set; }
        [AllowNull]
        public bool? notYetDue { get; set; }
        [AllowNull]
        public bool? cleared { get; set; }
        [AllowNull]
        public bool? onHold { get; set; }
        [AllowNull]
        public string? others { get; set; }
    }
}
