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

    }
}
