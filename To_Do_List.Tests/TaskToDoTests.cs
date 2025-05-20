using System.Collections.Generic;
using Xunit;
using To_Do_List;
using System.IO;
using System;

namespace To_Do_List.Tests
{
    public class TaskToDoTests
    {
        private static string GetTempFilePath() => Path.GetTempFileName();

        [Fact]
        public void AddTask_ShouldAddNewTaskToList()
        {
            // Arrange
            string tempFile = GetTempFilePath();
            var todo = new TaskToDo(tempFile);

            // Act
            Console.SetIn(new StringReader("Test Task"));
            todo.AddTask();

            // Assert
            var savedJson = File.ReadAllText(tempFile);
            Assert.Contains("Test Task", savedJson);
        }

        [Fact]
        public void MarkTaskAsDone_ShouldSetIsDoneToTrue()
        {
            // Arrange
            string tempFile = GetTempFilePath();
            var task = new TaskItem { Description = "Test", IsDone = false };
            File.WriteAllText(tempFile, System.Text.Json.JsonSerializer.Serialize(new List<TaskItem> { task }));

            var todo = new TaskToDo(tempFile);

            // Act
            Console.SetIn(new StringReader("1")); // Select first task
            todo.MarkTaskAsDone();

            // Assert
            var resultJson = File.ReadAllText(tempFile);
            Assert.Contains("\"IsDone\": true", resultJson);
        }

        [Fact]
        public void DeleteTask_ShouldRemoveTaskFromList()
        {
            // Arrange
            string tempFile = GetTempFilePath();
            var task = new TaskItem { Description = "DeleteMe", IsDone = false };
            File.WriteAllText(tempFile, System.Text.Json.JsonSerializer.Serialize(new List<TaskItem> { task }));

            var todo = new TaskToDo(tempFile);

            // Act
            Console.SetIn(new StringReader("1")); // Select first task
            todo.DeleteTask();

            // Assert
            var resultJson = File.ReadAllText(tempFile);
            Assert.DoesNotContain("DeleteMe", resultJson);
        }
    }
}

