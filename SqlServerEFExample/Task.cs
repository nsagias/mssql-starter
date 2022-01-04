using System;

namespace SqlServerEFExample {
  
  public class Task {
    public int TaskId { get; set; }
    public string Title { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsComplete { get; set; }
    public virtual User AssignedTo { get; set; }

    public override string ToString()
    {
      return @$"Task [ Id={this.TaskId}, Title={this.Title}, Due-Date={this.DueDate.ToString()}, IsComplete={this.IsComplete} ]";
    }
  }

}