using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public string TypeName { get; set; } = null!;
        public int Id { get; set; }
        public float TypeFactor { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
