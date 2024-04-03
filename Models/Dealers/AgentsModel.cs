using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Dealers
{
    public class AgentsModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string? name { get; set; }
        [AllowNull]
        public string? isDefault { get; set; }
        [DisallowNull]
        public int dealerId { get; set; }
    }
}
