using OefenExamen02.Models;

namespace OefenExamen02.Services
{
    public class InMemoryRealEstateData : IRealEstateData
    {
        static List<Property> _properties;

        static InMemoryRealEstateData()
        {
            _properties = new List<Property> {
                new Property {
                    Id = 1,
                    Address = "joskelaan 33",
                    City = "Kapellen",
                    NumberOfRooms = 1,
                    Price = 1000,
                    PropertyType = PropertyType.House,
                    IsSold = true,
                },new Property {
                    Id = 2,
                    Address = "kelaan 3",
                    City = "Kape",
                    NumberOfRooms = 4,
                    Price = 10008,
                    PropertyType = PropertyType.House,
                    IsSold = false,
                }, new Property {
                    Id = 3,
                    Address = "brutlaan 38",
                    City = "Kapellen",
                    NumberOfRooms = 10,
                    Price = 1500,
                    PropertyType = PropertyType.Apartment,
                    IsSold = false,
                }, new Property {
                    Id = 4,
                    Address = "joske 3",
                    City = "Kapellen",
                    NumberOfRooms = 3,
                    Price = 10500,
                    PropertyType = PropertyType.House,
                    IsSold = true,
                }, new Property {
                    Id = 5,
                    Address = "laan 33",
                    City = "ellen",
                    NumberOfRooms = 3,
                    Price = 1000,
                    PropertyType = PropertyType.House,
                    IsSold = true,
                }, new Property {
                    Id = 6,
                    Address = "joskelaan 330",
                    City = "Hoevenen",
                    NumberOfRooms = 4,
                    Price = 200,
                    PropertyType = PropertyType.Apartment,
                    IsSold = false,
                }
            };
        }

        public IEnumerable<Property> GetForSale()
        {
            return _properties.Where(x => x.IsSold == false);
        }

        public IEnumerable<Property> GetSold()
        {
            return _properties.Where(x => x.IsSold == true);
        }

        public Property Get(int id)
        {
            return _properties.FirstOrDefault(x => x.Id == id);
        }

        public Property Add(Property newProperty)
        {
            Random random = new Random();
            if ( 10 < random.Next(0,20))
            {
                throw new ArgumentException("Random exceptionez, Unlucky toch...");
            }
            // Method implementation here
            newProperty.Id = _properties.Count + 1;
            newProperty.IsSold = false;
            _properties.Add(newProperty);
            return _properties.FirstOrDefault(x => x.Id == newProperty.Id);
        }

        public void Update(Property property)
        {
            // Method implementation here
            Property result = Get(property.Id);
            if (result != null)
            {
                result.Price = property.Price;
                result.City = property.City;
                result.NumberOfRooms = property.NumberOfRooms;
                result.Address = property.Address;
                result.IsSold = property.IsSold;
            }
            return;
        }
    }

}
