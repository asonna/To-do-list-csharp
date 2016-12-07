using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ToDo.Objects
{
  public class Task // Same name as TABLE name but starting with Cap letter and without "s"
  {
    // where TaskId references a property of the object
    public int Id {get; set;}
    public string Description {get; set;}
    private List<string> allTasks = new List<string> {};
    //where Task references the Object

    public Task(string description, int id = 0)
    {
      this.Id = id;
      this.Description = description;
    }

    public override bool Equals(System.Object otherTask)
    {
      if (!(otherTask is Task))
      {
        return false;
      }
      else
      {
        Task newTask = (Task) otherTask;
        bool descriptionEquality = (this.Description == newTask.Description);
        return (descriptionEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.Description.GetHashCode();
    }


// Save() - In the cmd variable, we use the parameter placeholder @TaskDescription. We want to use parameter placeholders whenever we are entering data that a user enters. Information stored to a parameter is treated as field data and not part of the SQL statement, which helps to protect our application from SQL injection (nicely illustrated in this comic).
    public void Save()

    {
      SqlConnection conn = DB.Connection(); // A SqlConnection object basically represents the database using the connection information that we set it to
      conn.Open();  // Thi open the connection to the database so that the code can execute

      SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description) OUTPUT INSERTED.id VALUES (@TaskDescription);", conn); // SqlCommand objects are used to send SQL statements to the database. It takes two arguments: the command we want to execute, and the database connection the statement is being sent to. In this case they are (taskId, TaskDescription). - see C# database basics To do list with databases part 3 - working with data with ADO.NET

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@TaskDescription";
      descriptionParameter.Value = this.Description;
      cmd.Parameters.Add(descriptionParameter);

      SqlDataReader rdr = cmd.ExecuteReader(); // This command is actually executed when we use the ExecuteReader() method on cmd. The result set is the table

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close(); // This closes the connection to the database so that the code can execute
      }
    }

//Find() - Here we are using a SELECT query using WHERE id = @TaskId. We set @TaskId equal to the id that we pass into the Find() method, and convert it to a string so that it can be used in the query string. Then we read the result of the query to create a new Task named foundTask and return it.
    public static Task Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks WHERE id = @TaskId;", conn);
      SqlParameter taskIdParameter = new SqlParameter();
      taskIdParameter.ParameterName = "@TaskId";
      taskIdParameter.Value = id.ToString();
      cmd.Parameters.Add(taskIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundTaskId = 0;
      string foundTaskDescription = null;
      while(rdr.Read())
      {
        foundTaskId = rdr.GetInt32(0);
        foundTaskDescription = rdr.GetString(1);
      }
      Task foundTask = new Task(foundTaskDescription, foundTaskId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundTask;
    }


    public static List<Task> GetAll()
    {
      List<Task> allTasks = new List<Task>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM tasks;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        Task newTask = new Task(taskDescription);
        newTask.Id = taskId;
        allTasks.Add(newTask);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allTasks;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM tasks;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
