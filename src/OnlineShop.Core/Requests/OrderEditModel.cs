using System.ComponentModel.DataAnnotations;
using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Requests
{
    public record OrderEditModel
    (
        [Required] 
        OrderState State
    );
}