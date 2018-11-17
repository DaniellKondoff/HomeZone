using System;
using System.ComponentModel.DataAnnotations;

namespace HomeZone.Web.Models.LoanPropertyViewModels
{
    public class ReserveViewModel
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
