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
              // create use new table and insert rows using string builder
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
              // execute insertion into database and say done when complete
              using (SqlCommand command = new SqlCommand(sql, connection)) {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
              }

              // insert new row 
              Console.Write("Inserting a new row into table, press any key to continue");
              Console.ReadKey(true);

              // clear current string builder with variables
              sb.Clear();
              sb.Append("INSERT Employees (Name, Location");
              sb.Append("VALUES (@name, @location)");
              sql = sb.ToString();

              // create new command execute values
              using (SqlCommand command = new SqlCommand(sql, connection)) {
                // create command to add values anem and locations
                command.Parameters.AddWithValue("@name", "Bingo");
                command.Parameters.AddWithValue("@location", "Bingo Hall");
                // get the nummber of rows affected when commands is executed
                int rowsAffected = command.ExecuteNonQuery();
                // console write the lines to inform user on the command line
                Console.WriteLine($"{rowsAffected} row(s) inserted");
              }

              // update one user
              String userToUpdate = "Bob";
              Console.Write($"Updating 'location' for user '{userToUpdate}', pres any key to continue... ");
              Console.ReadKey(true);

              // clear previous string builder
              sb.Clear();
              sb.Append("UPDATE Employees SET Location = N'Peru' WHERE Name = @name");
              // convert to to string
              sql = sb.ToString();

              // create command to update value
              using (SqlCommand command = new SqlCommand(sql, connection)) {
                command.Parameters.AddWithValue("@name", userToUpdate);
                // get number after execute
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected}  row(s) updated");
              }

              // Delete one user
              String userToDelete ="Dingo";
              Console.Write($"Deleting user '{userToDelete}', press any key to continue.... ");
              Console.ReadKey(true);
              // clear string builder
              sb.Clear();
              sb.Append("DELETE FROM Employees WHERE Name = @name;");
              sql = sb.ToString();
              using (SqlCommand  command = new SqlCommand(sql, connection)) {
                command.Parameters.AddWithValue("@name", userToDelete);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) deleted");
              }



            }
  
          } catch (SqlException e) {

            Console.WriteLine(e.ToString());
          }
          Console.WriteLine("All done.  Press any key to finish ...");
          Console.ReadKey(true);
    }
    }
}
