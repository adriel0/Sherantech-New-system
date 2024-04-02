using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Receivables
{
    public class ReceivablesModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public int accountNo { get; set; }
        [AllowNull]
        public string? dealer { get; set; }
        [AllowNull]
        public string? bankName { get; set; }
        [AllowNull]
        public string checkNo { get; set; }
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
    }
}
