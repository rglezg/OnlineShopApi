using System.ComponentModel.DataAnnotations;
using OnlineShop.Core.Entities;

namespace OnlineShop.Core.Requests
{
    public record ProductModel
    (
        [Required] 
        string Name,
        
        [StringLength(Product.DescriptionMaxLength)]
        string Description,
        
        [Range(float.Epsilon, float.MaxValue)] 
        float Price,
        
        [Range(1, int.MaxValue)] 
        int Count
    )
    {
        public void Patch(Product product)
        {
            product.Name = Name;
            product.Description = Description;
            product.Count = Count;
            product.Price = Price;
        }
    }
}