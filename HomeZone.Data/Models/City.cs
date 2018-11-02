using System;
using System.Collections.Generic;
using System.Text;

namespace HomeZone.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Section> Sections { get; set; } = new List<Section>();
    }
}
