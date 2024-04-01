using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models.Srep
{
    public class SrepModel
    {
        [Key]
        public int Id { get; set; }
        [AllowNull]
        public string? name { get; set; }
    }
}
