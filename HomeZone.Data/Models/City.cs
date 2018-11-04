using System.Collections.Generic;

namespace HomeZone.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public ICollection<Property> Properties { get; set; } = new List<Property>();
    }
}
