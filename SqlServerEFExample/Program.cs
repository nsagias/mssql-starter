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
          builder.InitialCatalog = "master2";

          using (EFExampleContext context  = new EFExampleContext(builder.ConnectionString)) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.WriteLine("Create database and schema from Classes");

            // Create new user and save to DB
            User newUser = new User () { FirstName = "Nick", LastName = "Sagias"};
            context.Users.Add(newUser);
            context.SaveChanges();
            Console.WriteLine($"\nCreated User: {newUser.ToString()}");

            // Create new task and save to DB
            Task newTask = new Task() {Title = "Work out", IsComplete = false, DueDate = DateTime.Parse("02-02-2022")};
            context.Tasks.Add(newTask);
            context.SaveChanges();
            Console.WriteLine($"\nCreate Task {newTask.ToString()}");

            // Associate, assign task to user
            newTask.AssignedTo = newUser;
            context.SaveChanges();
            Console.WriteLine($"\nAssigned task to {newTask.Title} 'to user'  {newUser.GetFullName()}");

            // Find incomplete tassk and assign to user named "Nick"
            var query = from t in context.Tasks
                        where t.IsComplete == false && t.AssignedTo.FirstName.Equals("Nick")
                        select t;
            foreach(var t in query) {
              Console.WriteLine(t.ToString());
            }

            // Update due DueDate of a task
            Task taskToUpdate = context.Tasks.First(); // get the first task
            Console.WriteLine($"\nUpdating task: {taskToUpdate.ToString()}");
            taskToUpdate.DueDate = DateTime.Parse("02-06-2022");
            context.SaveChanges();
            Console.WriteLine($"Duedate Changed {taskToUpdate.ToString()}");

            // Delete all tasks with a dueDate in 2022
            Console.WriteLine("\nDelete All tasks with a DueDate of 2022");
            DateTime dueDate2022 = DateTime.Parse("12-31-2022");
            query = from t in context.Tasks
                    where t.DueDate < dueDate2022
                    select t;
            foreach(Task t in query) {
              Console.WriteLine($"Deleting task: {t.ToString()}");
              context.Tasks.Remove(t);
            }
            context.SaveChanges();

            // Get all after delete
            Console.WriteLine("\nTasks after delete");
            List<Task> tasksAfterDelete = (from t in context.Tasks select t).ToList<Task>();
            if (tasksAfterDelete.Count == 0) {
              Console.WriteLine("[TASK LIST IS EMPTY]");
            } else {
              foreach (Task t in query) {
                Console.WriteLine(t.ToString());
              }
            }

          }
          
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }

        Console.WriteLine("All done. Press any key to finish...,");
        Console.ReadKey(true);
        
      }
    }
}
