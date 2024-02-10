using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class DealersModel
    {
        [Key]
        public int Id { get; set;}
        [AllowNull]
        public string? DealerName { get; set;}
        [AllowNull]
        public string? DealerAddress { get; set;}
        [DisallowNull]
        public int DealerAccountId { get; set;}
    }
}
