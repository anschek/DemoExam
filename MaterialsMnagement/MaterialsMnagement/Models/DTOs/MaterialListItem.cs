using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialsMnagement.Models.DTOs
{
    public class MaterialListItem
    {
        public string Name { get; set; } = null!;
        public string TypeName { get; set; } = null!;
        public int Amount { get; set; } 
        public int MinimalAmount { get; set; } 
        public decimal Cost { get; set; }
        public string Description { get; set; } = null!;
        public Bitmap? Image { get; set; }
    }
}
