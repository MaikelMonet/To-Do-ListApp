using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace To_Do_List
{
    public class TaskToDo
    {
        private readonly string filePath;

        private List<TaskItem>? availableTask = new List<TaskItem>();

        public TaskToDo(string filepath)
        {
            this.filePath = filepath;
        }

        public void ViewTask()
        {
            CheckFileContent();

            Console.WriteLine();

            DisplayListContent();

            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        public void AddTask()
        {
            LoadTasks();

            Console.WriteLine("What is the new task?");

            TaskItem newTask = new TaskItem
            {
                IsDone = false,
                Description = Console.ReadLine()
            };

            availableTask?.Add(newTask);

            SaveTaskToFile();
        }

        public void MarkTaskAsDone()
        {
            LoadTasks();

            if (availableTask?.Count > 0)
            {
                DisplayListContent();

                Console.WriteLine("Which task would you like to complete, type the task number\n");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= availableTask?.Count)
                {
                    // index is 0 base counting, we must substract 1.
                    availableTask[index - 1].IsDone = true;
                    Console.WriteLine("Task is now complete");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Invalid selection.\n Press enter to continue....");
                    Console.ReadLine();
                }

                SaveTaskToFile();
            }
            else
            {
                Console.WriteLine("There are no available task at the moment... relax you earned it!");
                Console.WriteLine("Press enter to continue....");
                Console.ReadLine();
            }
        }

        public void DeleteTask()
        {
            LoadTasks();

            if (availableTask?.Count > 0)
            {
                DisplayListContent();
 
                Console.WriteLine("Which task would you like to delete, type the task number\n");

                if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= availableTask?.Count)
                {
                    // Because the index would be 0 base counting, we must substract 1.
                    availableTask.Remove(availableTask[index - 1]);
                    Console.WriteLine("Task has now been removed");
                }

                SaveTaskToFile();
            }
            else
            {
                Console.WriteLine("There are no available task at the moment... relax you earned it!");
                Console.WriteLine("Press enter to continue....");
            }

        }

        private void LoadTasks()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                try
                {
                    availableTask = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
                }
                catch
                {
                    availableTask = new List<TaskItem>();
                }
            }
            else
            {
                availableTask = new List<TaskItem>();
            }
        }


        private void SaveTaskToFile()
        {
            //Serialize the List to JSON
            JsonSerializerOptions option = new JsonSerializerOptions { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(availableTask, option);

            File.WriteAllText(filePath, jsonString);

        }

        private void CheckFileContent()
        {
            // Read JSON from file
            string jsonFile = File.ReadAllText(filePath);

            if (jsonFile.Length > 3)
            {
                //Deserialize into a list of TaskItem
                availableTask = JsonSerializer.Deserialize<List<TaskItem>>(jsonFile);
            }
            else
            {
                Console.WriteLine("There are no available task at the moment... relax you earned it!");
            }
        }

        private void DisplayListContent()
        {
            int taskNumber = 1;

            foreach (TaskItem item in availableTask)
            {
                if (item.IsDone == true)
                {
                    Console.WriteLine($"TaskNumber: ({taskNumber++}), Description: {item.Description}, Created at: ({item.CreatedAt}), (Complete)");
                }
                else
                {
                    Console.WriteLine($"TaskNumber: ({taskNumber++}), Description: {item.Description}, Created at: ({item.CreatedAt}), (Incomplete)");
                }

            }
        }
    }
}
