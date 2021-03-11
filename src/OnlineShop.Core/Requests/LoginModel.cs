using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Requests
{
    public record LoginModel
    (
        [Required]
        string Username,
        
        [Required]
        string Password
    );
}