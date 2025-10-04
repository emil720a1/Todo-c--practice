using To_do;
using Task = To_do.Task;

class Program
{
    static void Main()
    {
        ToDoList list = new ToDoList();

        if (File.Exists("tasks.txt"))
        {
            if (list.LoadFromFile("tasks.txt"))
            {
              Console.WriteLine("Завдання успішно завантажено!");
            }
        }
        else
        {
            Console.WriteLine("Починаємо з порожнім списком");
        }

        bool running = true;
        while (running)
        {

            Console.WriteLine("\n=== TODO List ===");
            Console.WriteLine("1.Додати завдання");
            Console.WriteLine("2.Видалити завдання");
            Console.WriteLine("3.Позначити як виконане");
            Console.WriteLine("4.Показати всі завдання");
            Console.WriteLine("5.Показати завдання за категорією");
            Console.WriteLine("6.Показати завдання за пріоритетом");
            Console.WriteLine("7.Вихід");
            Console.Write("Ваш вибір: ");

            // string choice = Console.ReadLine();

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7)
            {
                Console.WriteLine("Невірний вибір, введіть число віж 1 до 7:");
                continue;
            }

        switch (choice)
            {
                case 1:
                    Console.WriteLine("Введіть Ім'я");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введіть Опис");
                    string description = Console.ReadLine();
                    Console.WriteLine("Введіть Категорію");
                    Category category;
                    try
                    {
                        category = Enum.Parse<Category>(Console.ReadLine());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Введена некоректна категорія");
                        continue;
                    }

                    Console.WriteLine("Введіть пріоритет: 1.Low, 2.Medium, 3.High");
                    Priority priority;
                    try
                    {
                        priority = Enum.Parse<Priority>(Console.ReadLine());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Введений некоректний пріоритет");
                        continue;
                    }

                    Console.WriteLine("Введіть дедлайн(yyyy-MM-dd) або залиште порожнім");
                    string dueDateInput = Console.ReadLine();
                    DateTime? dueDate = null;
                    if (!string.IsNullOrWhiteSpace(dueDateInput)) 
                    {
                         if (DateTime.TryParse(dueDateInput, out DateTime parsedDate))
                         {
                             dueDate = parsedDate;
                         }
                         else
                         {
                             Console.WriteLine("Невірний формат дати!");
                             continue;
                         }
                    }

                    if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
                    {
                        Console.WriteLine("Поля не можуть бути порожніми");
                        continue;
                    }

                    Task tasks = new Task(name, description, priority, dueDate);
                    tasks.Category = category;
                    list.AddTask(tasks);

                    Console.WriteLine("✓ Завдання додано!");
                    break;
                case 2:
                    var allTasks1 = list.GetTask();
                    if (allTasks1.Count > 0)
                    {
                        foreach (var task in allTasks1)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Немаэ завдань для видалення");
                    }
                    Console.WriteLine("Введіть id задачі яку ви хочете видалити");
                    int id = 0;
                    try
                    {
                        id = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Невірний формат ID, введіть число");
                        continue;   
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Число занадто велике або занадто мале");
                        continue;   
                    }

                    if (list.RemoveTask(id))
                    {
                      Console.WriteLine("Завдання видалено!");
                    }
                    else
                    {
                        Console.WriteLine("Задачу з таким ID не знайдено");
                    }
                    
                    
                    break;
                case 3:
                    int idCompleted = 0;
                    var allTasks2 = list.GetTask();
                    if (allTasks2.Count > 0)
                    {
                        foreach (var task in allTasks2)
                        {
                           Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Немає завдань для видалення");
                    }

                    Console.WriteLine("Введіть id задачі яку ви хочете позначити як виконану");
                    try
                    {

                        idCompleted = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Невірний формат задачі яку ви хочете позначити як виконану");
                        continue;   
                    }

                    if (list.MarkTaskCompleted(idCompleted))
                    {
                        Console.WriteLine("Завдання позначене як виконане!");
                    }
                    else
                    {
                        Console.WriteLine("Завдання не знайдено!");   
                    }

                    break;
                    
                case 4:
                    var res1 = list.GetTask();
                    if (res1.Count > 0)
                    {
                        foreach (var task in res1)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Немає завдань!");
                    }
                    break;
                
                case 5:
                    Console.WriteLine("Введіть категорію");
                    string categoryName = Console.ReadLine();
                    Category category1;
                    try
                    {

                        category1 = Enum.Parse<Category>(categoryName);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Введена некоректна категорія");
                        continue; 
                    }
                    
                    var taskByCategory = list.GetTaskByCategory(category1);
                    if (taskByCategory.Count > 0)
                    {
                        foreach (var task in taskByCategory)
                        {
                            string taskInfo =task.ToString();
                            Console.WriteLine(taskInfo);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Завдання з такою категорією відсутні!");
                    }

                    break;
                case 6:
                    
                    Console.WriteLine("Введіть пріоритет: 1.Low, 2.Medium, 3.High");
                    Priority priority1;
                    try
                    {
                        priority1 = Enum.Parse<Priority>(Console.ReadLine());
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("Введений некоректний пріоритет");
                        continue;
                    }

                    var taskByPriority = list.GetTaskByPriority(priority1);
                    if (taskByPriority.Count > 0)
                    {
                        foreach (var task in taskByPriority)
                        {
                            Console.WriteLine(task.ToString());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Завдання з таким пріоритетом відсутні!");
                    }
                    break;
                    
                case 7:
                    running = false;
                    break;
                default:
                    break;
            }
            list.SaveToFile("tasks.txt");
        }
    }
}