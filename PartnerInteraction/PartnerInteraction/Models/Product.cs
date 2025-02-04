using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class Product
    {
        public Product()
        {
            PartnersProducts = new HashSet<PartnersProduct>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Articul { get; set; } = null!;
        public decimal MinCostForPartner { get; set; }

        public virtual ProductType Type { get; set; } = null!;
        public virtual ICollection<PartnersProduct> PartnersProducts { get; set; }
    }
}
