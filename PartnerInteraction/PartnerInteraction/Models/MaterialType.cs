using System;
using System.Collections.Generic;

namespace PartnerInteraction.Models
{
    public partial class MaterialType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public float DefectsPercentage { get; set; }
    }
}
