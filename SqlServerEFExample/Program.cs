using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace SqlServerEFExample
{
    class Program {
      static void Main(string[] args){
        
        Console.WriteLine("CRUD sample with EntitityFramework 3.1 and Sql Server");

        try {
          // connection string
          SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
          builder.DataSource = "localhost";
          builder.UserID = "sa";
          builder.Password = "SuperSecret1";
          builder.InitialCatalog = "master";
          
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }

        Console.WriteLine("All done. Press any key to finish...,");
        Console.ReadKey(true);
        
      }
    }
}
