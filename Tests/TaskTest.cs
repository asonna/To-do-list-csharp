using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ToDo.Objects; // Not needed always

namespace  ToDo
{
  public class ToDoTest : IDisposable
  {
    public ToDoTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=ToDo_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Save_SavesToDatabase_true()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn");

      //Act
      testTask.Save();
      List<Task> result = Task.GetAll();
      List<Task> testList = new List<Task>{testTask};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_true()
    {
      //Arrange, Act
      int result = Task.GetAll().Count;

      //Assert
      Assert.Equal(true, result == 0);
    }

    [Fact]
    public void Equal_AreObjectsEquivalent_true()
    {
      //Arrange
      Task firstTask = new Task("Buy lettuce.");
      Task secondTask = new Task("Buy lettuce.");
      //Act
      //Assert
      Assert.Equal(firstTask, secondTask);
    }

    [Fact]
    public void Find_FindsTaskInDatabase_true()
    {
      //Arrange
      Task testTask = new Task("Mow the lawn");
      testTask.Save();

      //Act
      Task foundTask = Task.Find(testTask.Id);

      //Assert
      Assert.Equal(testTask, foundTask);
    }

    public void Dispose()
    {
      Task.DeleteAll();
    }

  }
}
