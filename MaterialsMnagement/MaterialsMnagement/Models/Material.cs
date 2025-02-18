using System;
using System.Collections.Generic;

namespace MaterialsMnagement.Models;

public partial class Material
{
    public int Id { get; set; }

    public int Type { get; set; }

    public string Name { get; set; } = null!;

    public int Amount { get; set; }

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public int MinimalAmount { get; set; }

    public string Unit { get; set; } = null!;

    public int PackQuantity { get; set; }

    public byte[]? Image { get; set; }

    public virtual MaterialType TypeNavigation { get; set; } = null!;

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
