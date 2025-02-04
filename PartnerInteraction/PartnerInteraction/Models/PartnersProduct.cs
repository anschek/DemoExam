using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class PartnersProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int PartnerId { get; set; }
        public int Amount { get; set; }
        public DateTime DateOfSale { get; set; }

        public virtual Partner Partner { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
