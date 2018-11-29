using HomeZone.Services;
using HomeZone.Services.Models.LoanProperties;
using System;
using System.Collections.Generic;

namespace HomeZone.Web.Models.LoanPropertyViewModels
{
    public class LoanPropertyListingModel
    {
        public IEnumerable<ListingPropertyServiceModel> Properties { get; set; }

        public int TotalProperties { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalProperties / ServiceConstants.PropertyListingPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
