using GoSaveMe.Commons.Configuration;
using GoSaveMe.Commons.Models.Database;
using System.Data;
using System.Data.SqlClient;

namespace GoSaveMe.Commons.Processors
{
    public class DatabaseProcessor
    {
        public class CountriesDB
        {
            public static List<Country>? Get()
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetCountries, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Country> countries = new GenericPopulator<Country>().CreateList(reader);

                sqlConnection.Close();

                return countries;
            }

            public static Country? Get(int id)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetCountry, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                Country? country = new GenericPopulator<Country>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return country;
            }
        }

        public class UserDB
        {
            public static List<User>? Get(string? username, bool? isOrg)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetUser, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;
                sqlCommand.Parameters.Add("@IsOrg", SqlDbType.Bit).Value = isOrg;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                List<User> users = new GenericPopulator<User>().CreateList(reader);

                sqlConnection.Close();

                return users;
            }

            public static User? Create(string username)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.CreateUser, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = username;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                User? user = new GenericPopulator<User>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return user;
            }

            public static User? Update(User user)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.UpdateUser, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                sqlCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                sqlCommand.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = user.LastName;
                sqlCommand.Parameters.Add("@Citizenship", SqlDbType.NVarChar).Value = user.Citizenship;
                sqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = user.Address;
                sqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar).Value = user.Description;
                sqlCommand.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar).Value = user.PhoneNumber;
                sqlCommand.Parameters.Add("@IsPrivileged", SqlDbType.NVarChar).Value = user.IsPrivileged;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                User? updatedUser = new GenericPopulator<User>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return updatedUser;
            }
        }

        public class NewsDB
        {
            public static News? Get(int id)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetNews, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                News? news = new GenericPopulator<News>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return news;
            }

            public static List<News>? GetFilteredNews(bool approved, string? username)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetFilteredNews, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Approved", SqlDbType.Bit).Value = approved;
                sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                List<News> news = new GenericPopulator<News>().CreateList(reader);

                sqlConnection.Close();

                return news;
            }

            public static News? Create(string title, string text, string imageUri, string username, bool approved)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.CreateNews, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                sqlCommand.Parameters.Add("@Text", SqlDbType.NVarChar).Value = text;
                sqlCommand.Parameters.Add("@ImageURI", SqlDbType.NVarChar).Value = imageUri;
                sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                sqlCommand.Parameters.Add("@Approved", SqlDbType.Bit).Value = approved;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                News? news = new GenericPopulator<News>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return news;
            }

            public static News? Approve(int id)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.ApproveNews, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                News? news = new GenericPopulator<News>().CreateList(reader).FirstOrDefault();

                sqlConnection.Close();

                return news;
            }

            public static void Delete(int id)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.DeleteNews, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }

        }

        public class FoundraisingDB
        {
            public static List<Foundraising>? Get()
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetFoundraisings, sqlConnection);

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Foundraising>? foundraisings = new GenericPopulator<Foundraising>().CreateList(reader);

                sqlConnection.Close();

                return foundraisings;
            }
        }

        public class SafepointDB
        {
            public static List<Safepoint>? Get()
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return null;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.GetSafepoints, sqlConnection);

                sqlConnection.Open();

                using SqlDataReader reader = sqlCommand.ExecuteReader();

                List<Safepoint>? safepoints = new GenericPopulator<Safepoint>().CreateList(reader);

                sqlConnection.Close();

                return safepoints;
            }
        }

        public class NewsReactionDB
        {
            public static void Create(int newsId, string username, string reaction)
            { 
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.AddNewsReaction, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@NewsId", SqlDbType.Int).Value = newsId;
                sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                sqlCommand.Parameters.Add("@Reaction", SqlDbType.NVarChar).Value = reaction;

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }

            public static void Delete(int newsId, string username, string reaction)
            {
                GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

                if (config == null)
                    return;

                using SqlConnection sqlConnection = new(config.DbConnectionString);

                SqlCommand sqlCommand = new(Constants.StoredProcedures.DeleteNewsReaction, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@NewsId", SqlDbType.Int).Value = newsId;
                sqlCommand.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;
                sqlCommand.Parameters.Add("@Reaction", SqlDbType.NVarChar).Value = reaction;

                sqlConnection.Open();

                sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
            }
        }
    }
}
