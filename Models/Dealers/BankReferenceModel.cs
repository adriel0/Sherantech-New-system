using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Dealers
{
    public class BankReferenceModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string? accountNo { get; set; }
        [AllowNull]
        public string? accountName { get; set; }
        [AllowNull]
        public string? type { get; set; }
        [AllowNull]
        public string? bank { get; set; }
        [DisallowNull]
        public int dealerId { get; set; }
    }
}
