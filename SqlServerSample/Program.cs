using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.UserID = "sa";
            builder.Password = "secret";
            builder.InitialCatalog = "master";

            // connect to mssql server
            Console.WriteLine("Conecting to MSSQL ....");
            using (SqlConnection connection = new SqlConnection(builder.ConnectionString)){
              connection.Open();
              Console.WriteLine("Connected.");
            }

            Console.WriteLine("All done.  Press any key to finish ...");
            Console.ReadKey(true);
        }
    }
}
