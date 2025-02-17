
namespace PartnerInteraction.Models.DTOs
{
    public class PartnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;
        public short Rating { get; set; }
        public ushort Discount { get; set; }
    }
}
