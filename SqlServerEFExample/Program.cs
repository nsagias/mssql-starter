using System;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


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

          using (EFExampleContext context  = new EFExampleContext(builder.ConnectionString)) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine("Create database and schema from Classes");

            // Create new user and save to DB
            User newTask = new User () { FirstName = "Nick", LastName = "Sagias"};
            context.Users.Add(newUser);
            context.SaveChanges();
            Console.WriteLine($"\nCreated User: {newUser.ToString()}");

            // Create new task and save to DB
            Task newTask = new Task() {Title = "Work out", IsComplete = false, DateTime.Parse("02-02-2022")};
            context.Tasks.Add(newTask);
            context.SaveChanges();
            Console.WriteLine($"\nCreate Task {newTask.ToString()}");

            // Associate, assign task to user
            newTask.AssignedTo = newUser;
            context.SaveChanges();
            Console.WriteLine($"\nAssigned task to {newTask.Title} 'to user'  {newUser.GetFullName()}");
          }
          
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }

        Console.WriteLine("All done. Press any key to finish...,");
        Console.ReadKey(true);
        
      }
    }
}
