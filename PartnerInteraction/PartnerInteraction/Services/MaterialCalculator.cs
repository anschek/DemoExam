using PartnerInteraction.Models;

namespace PartnerInteraction.Services
{
    public static class MaterialCalculator
    {
        public static int Calculate(int productTypeId, int materialTypeId, int productAmount,
            float width, float height)
        {
            if(width <= 0 || height <= 0) return -1;

            DemoContext db = new DemoContext();

            var productType = db.ProductTypes.Find(productTypeId);
            var materialType = db.MaterialTypes.Find(materialTypeId);

            if (productType == null || materialType == null) return -1;

            float defectRate = materialType.DefectsPercentage;
            float productTypeRate = productType.TypeFactor;

            return (int)(productAmount * width * height * productTypeRate * (1+defectRate));
        }
    }
}
