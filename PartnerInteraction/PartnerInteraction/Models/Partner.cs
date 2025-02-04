using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class Partner
    {
        public Partner()
        {
            PartnersProducts = new HashSet<PartnersProduct>();
        }

        public int Id { get; set; }
        public int PartnerTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string? Director { get; set; }
        public string Email { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public string Inn { get; set; } = null!;
        public short Rating { get; set; }
        public string? LegalAddress { get; set; }

        public virtual PartnerType PartnerType { get; set; } = null!;
        public virtual ICollection<PartnersProduct> PartnersProducts { get; set; }
    }
}
