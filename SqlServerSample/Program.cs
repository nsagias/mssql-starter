﻿using System;
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
              String sql = "DROP DATABASE IF EXISTS [firstExampleDB]; CREATE DATABASE [firstExampleDB";
              
              // create command/cmd for sql
              using (SqlCommand command = new SqlCommand(sql, connection)) {
                command.ExecuteNonQuery();
                Console.WriteLine("Done.");
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
