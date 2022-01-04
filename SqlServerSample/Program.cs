using System;
using System.Text;
using System.Data.SqlClient;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
          try {
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

              // create a sample database
              Console.Write("Dropping and creating database sample data .....");
              String sql = "DROP DATABASE IF EXISTS [FirstExampleDB]; CREATE DATABASE [FirstExampleDB";
              
              // create command/cmd for sql
              using (SqlCommand command = new SqlCommand(sql, connection)) {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
              }
              // create table and insert mock data manually
              Console.Write("Creating smaple table with data, press any key to continue");
              Console.ReadKey(true);
              // create new string builder
              StringBuilder sb = new StringBuilder();
              sb.Append("USE FirstExampleDB; ");
              sb.Append("CREATE TABLE Employees ( ");
              sb.Append(" Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
              sb.Append(" Name NVARCHAR(50), ");
              sb.Append(" Location NVARCHAR(50) ");
              sb.Append("); ");
              sb.Append("INSERT INTO Employees (Name, Location) VALUES ");
              sb.Append("(N'Nick', N'Canada'), ");
              sb.Append("(N'Bob', N'Lost Country'), ");
              sb.Append("(N'Dingo', N'Australia'), ");
              sql = sb.ToString();


            }
  
          } catch (SqlException e) {

            Console.WriteLine(e.ToString());
          }
          Console.WriteLine("All done.  Press any key to finish ...");
          Console.ReadKey(true);
    }
    }
}
