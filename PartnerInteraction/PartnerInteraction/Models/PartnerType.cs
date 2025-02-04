using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class PartnerType
    {
        public PartnerType()
        {
            Partners = new HashSet<Partner>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Partner> Partners { get; set; }
    }
}
