using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HomeZone.Web.Infrastructure.Common
{
    public static class DropDownListUtility
    {
        public static IEnumerable<SelectListItem> GetSpaceDropDown()
        {
            return new List<SelectListItem>
            {
            new SelectListItem{ Text="-",  Value = "-1", Selected= true },
            new SelectListItem{ Text="50",  Value = "50",},
            new SelectListItem{ Text="100", Value = "100" },
            new SelectListItem{ Text="150", Value = "150" },
            new SelectListItem{ Text="200", Value = "200" },
            new SelectListItem{ Text="250", Value = "250" },
            new SelectListItem{ Text="300", Value = "300" },
            new SelectListItem{ Text="400", Value = "400" },
            new SelectListItem{ Text="500", Value = "500" },
            };
        }

        public static IEnumerable<SelectListItem> GetPriceDropDown()
        {
            return new List<SelectListItem>
            {
            new SelectListItem{ Text="-",  Value = "-1", Selected= true },
            new SelectListItem{ Text="50",  Value = "50",},
            new SelectListItem{ Text="100", Value = "100" },
            new SelectListItem{ Text="200", Value = "200" },
            new SelectListItem{ Text="400", Value = "400" },
            new SelectListItem{ Text="500", Value = "500" },
            new SelectListItem{ Text="1000", Value = "1000" },
            new SelectListItem{ Text="2000", Value = "2000" },
            new SelectListItem{ Text="4000", Value = "4000" },
            new SelectListItem{ Text="8000", Value = "8000" },
            };
        }
    }
}
