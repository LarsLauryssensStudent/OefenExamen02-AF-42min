using OefenExamen02.Models;

namespace OefenExamen02.Services
{
    public interface IRealEstateData
    {
        public IEnumerable<Property> GetForSale();

        public IEnumerable<Property> GetSold();

        public Property Get(int id);

        public Property Add(Property newProperty);

        public void Update(Property property);

    }
}
