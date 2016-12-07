using Nancy;
using System.Collections.Generic;
using System;
using ToDo.Objects;

namespace ToDo
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["ToDo.cshtml"];
      };

    }
  }
}


//---------------------------------------------------------------------------------
//Additional instructions: When we use parameters in our queries, there are three steps we need to take:
//
// 1. Create the SqlCommand query with parameters:
//
// SqlCommand cmd = new SqlCommand("INSERT INTO tasks (description) OUTPUT INSERTED.id VALUES (@TaskDescription);", conn);
// The placeholder @TaskDescription will be replaced with actual data from the user when the SqlCommand executes. Parameter placeholders need the @ symbol prefixing the name.
//
// 2. Declare a SqlParameter object and assign values:
//
// SqlParameter descriptionParameter = new SqlParameter();
// descriptionParameter.ParameterName = "@TaskDescription";
// descriptionParameter.Value = this.GetDescription();
// We need to create a SqlParameter object for each parameter that we use in our SqlCommand. The ParameterName needs to match the parameter in the command string. The Value is what will replace the parameter in the command string when it is executed.
//
// 3. Add the SqlParameter object to the SqlCommand object's Parameters property:
//
// cmd.Parameters.Add(descriptionParameter);
// We let the SqlCommand object know about the SqlParameter when we add it to the SqlCommand's Parameters property using Add() and passing in the parameter as its argument. If we had more parameters to add, we would need to Add each one.
//
// One more thing about the query. Our command includes OUTPUT INSERTED.id. Because the ID is the Identity column, it is automatically generated when we add a new task, and we need some way to retrieve this information to use in our app. OUTPUT INSERTED.id will give us the id.
//
// Finally, we execute the SQL query with rdr = cmd.ExecuteReader(); like we did for GetAll().
//
// Since the output of our query is the task's ID, we want to save that value to our instance variable with
//
// while (rdr.Read())
// {
//   this._id = rdr.GetInt32(0);
// }
