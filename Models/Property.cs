using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OefenExamen02.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string City { get; set; } = string.Empty;
        public int NumberOfRooms { get; set; }
        public PropertyType PropertyType { get; set; }
        public decimal Price { get; set; }

        [DefaultValue(false)]
        public bool IsSold { get; set; }
    }

}
