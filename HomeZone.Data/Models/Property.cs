using System.Collections.Generic;

namespace HomeZone.Data.Models
{
    public class Property
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }

        public int Space { get; set; }

        public int CityId { get; set; }

        public City City { get; set; }

        public bool IsSold { get; set; }

        public bool IsForRent { get; set; }

        public int SectionId { get; set; }

        public Section Section { get; set; }

        public byte[] MainImage { get; set; }

        public byte[] SecondaryImage { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
