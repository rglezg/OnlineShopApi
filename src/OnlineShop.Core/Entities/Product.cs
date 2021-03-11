using System;
using Slugify;

namespace OnlineShop.Core.Entities
{
    public class Product : IOwnedResource
    {
        public const int DescriptionMaxLength = 100;
        
        private string _name;
        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                Slug = new SlugHelper().GenerateSlug(_name);
            }
        }

        public string Description { get; set; }
        public int Count { get; set; }
        public string Slug { get; set; }
        public float Price { get; set; }
        public string OwnerId { get; set; }
    }
}