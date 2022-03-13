namespace FlightManager.Common
{
    /// <summary>
    /// This class contains the all of the global constants that the project uses.
    /// </summary>
    public static class GlobalConstants
    {
        public const string SystemName = "Flight Manager";
        public const string AdministrationArea = "Administration";

        public const string LongDateFormat = "dd/MM/yyyy HH:mm";
        public const int PaginationOffset = 5;
        public const int DefaultPage = 1;
        public const int DefaultItemPerPage = 10;
        public const string QueryStringDelimiter = "&";
        public const string PageKey = "page";
        public const string ItemsPerPageKey = "itemsPerPage";

        public static class Roles
        {
            public const string Administrator = "Administrator";

            public const string Employee = "Employee";
        }

        public static class EmailCredentials
        {
            public static string Email { get; set; }
        }

        public static class Email
        {
            public const string SystemEmail = "flight.manager14@gmail.com";

            public const string ApiKey = "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";
        }

        public static class EmailSubjects
        {
            public const string PassengerConfirmationEmail = "Ticket reservation confirmation.";

            public const string ClientConfirmationEmail = "Tickets reservation confirmation.";
        }

        public static class Admin
        {
            public const string Username = "admin";

            public const string Password = "123456";

            public const string Email = "admin@mail.com";

            public const string Name = "Admin";

            public const string Surname = "Root";

            public const string PersonalNumber = "0123456789";

            public const string Address = "Smolyan, Bulgaria";

            public const string PhoneNumber = "0877887780";
        }
    }
}
