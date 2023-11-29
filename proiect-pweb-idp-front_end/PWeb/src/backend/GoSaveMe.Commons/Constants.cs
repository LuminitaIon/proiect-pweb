namespace GoSaveMe.Commons
{
    public class Constants
    {
        public const string AppName = "GoSaveMe";

        public class StoredProcedures
        {
            public const string CreateNews = "CreateNews";
            public const string CreateUser = "CreateUser";
            public const string GetUser = "GetUser";
            public const string UpdateUser = "UpdateUser";
            public const string GetCountries = "GetCountries";
            public const string GetCountry = "GetCountry";
            public const string GetNews = "GetNews";
            public const string GetFilteredNews = "GetFilteredNews";
            public const string GetSafepoints = "GetSafepoints";
            public const string GetFoundraisings = "GetFoundraisings";
            public const string ApproveNews = "ApproveNews";
            public const string DeleteNews = "DeleteNews";
            public const string AddNewsReaction = "AddNewsReaction";
            public const string DeleteNewsReaction = "DeleteNewsReaction";
        }

        public class Status
        {
            public const string Success = "success";
            public const string Error = "error";
        }
    }
}