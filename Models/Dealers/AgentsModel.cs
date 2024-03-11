using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Dealers
{
    public class AgentsModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string? lastName { get; set; }
        [AllowNull]
        public string? firstName { get; set; }
        [AllowNull]
        public bool? isDefault { get; set; }
    }
}
