namespace BizBadgeApp.Helper
{
    public class DbConnectionStringcs
    {
        //public static string GetConnectionString()
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .Build();

        //    return config.GetConnectionString("DefaultConnection");
        //}
        private readonly IConfiguration _connectionString;
        public DbConnectionStringcs(IConfiguration connectionString) 
        {
            _connectionString = connectionString;
        }
        public  string GetConnectionStrig()
        {
            return _connectionString.GetConnectionString("DefaultConnection");

        }
    }
}