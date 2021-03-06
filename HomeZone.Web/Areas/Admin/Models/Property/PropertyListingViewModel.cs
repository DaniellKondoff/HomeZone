﻿using HomeZone.Services;
using HomeZone.Services.Admin.Models.Properties;
using System;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Property
{
    public class PropertyListingViewModel
    {
        public IEnumerable<AdminPropertyListinServiceModel> Properties { get; set; }

        public int TotalProperties { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalProperties / ServiceConstants.AdminPropertyListingPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
