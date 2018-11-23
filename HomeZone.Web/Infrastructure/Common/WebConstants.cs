namespace HomeZone.Web.Infrastructure.Common
{
    public static class WebConstants
    {
        public const string AdministratingRole = "Administrator";
        public const string AdministratingEmail = "admin@admin.com";
        public const string AdministratingPassword = "admin123";

        public const string TempDataSuccessMessageKey = "SuccessMessage";
        public const string TempDataErrorMessageKey = "ErrorMessage";

        public const string AdminArea = "Admin";

        public const string WithId = "{id}";
        public const string GetLocationsByCityIdRoute = "ByCityId/{id}";

        public const string PropertyTable = "PropertyTable";
        public const string UserTable = "UserTable";
        public const string CityTable = "CityTable";
        public const string LocationTable = "LocationTable";
    }
}
