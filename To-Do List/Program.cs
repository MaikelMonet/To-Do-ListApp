using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_List
{
    public class Program
    {
        const string filePath = "Task.txt";

        static void Main()
        {
            TaskToDo task = new TaskToDo(filePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== To-Do List ====");
                Console.WriteLine("1. View Tasks");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Mark Task as Done");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                switch (Console.ReadLine())
                {
                    case ("1"):
                        task.ViewTask();
                        break;
                    case ("2"):
                        task.AddTask();
                        break;
                    case ("3"):
                        task.MarkTaskAsDone();
                        break;
                    case ("4"):
                        task.DeleteTask();
                        break;
                    case ("5"):
                        return;
                    default:
                        Console.WriteLine("Don't recognise this choice? \n");
                        Console.WriteLine("Press enter to continue.....\n");
                        Console.ReadLine();
                       break;

                }
            }
        }
    }
}
