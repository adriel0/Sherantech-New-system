using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Key]
        public string UserName { get; set; }
        [DisallowNull]
        public string? UserPassword { get; set; }

    }
}
