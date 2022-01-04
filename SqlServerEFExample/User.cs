using System;
using System.Collections.Generic;

namespace SqlServerEFExample
{
    public class User {
      public int UserId { get; set; }
      public String FirstName { get; set; }
      public String LastName { get; set; }
      public virtual IList<Task> Task { get; set; }

      // return full name string
      public String GetFullName() {
        return $"{this.FirstName} {this.LastName}";
      }
      // return var in a string
      public override string ToString() {
        return $"User [ id={this.UserId}, name={this.GetFullName()} ]";
      }

    }
}
