using System;

namespace HomeZone.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Property Property { get; set; }

        public int PropertyId { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }
    }
}
