using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerInteraction.Models.DTOs
{
    public class ReleaseDto
    {
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public string ReleaseDate { get; set; }
        public int Amount { get; set; }
    }
}
