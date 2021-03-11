using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Core.Requests
{
    public record OrderAddModel
    (
        [Required] 
        int ProductId,
        
        [Required] 
        [Range(1, int.MaxValue)] 
        int Count
    );
}