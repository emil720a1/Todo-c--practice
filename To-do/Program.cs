using To_do;
using Task = To_do.Task;

class Program
{
    static void Main()
    {
        ToDoList list = new ToDoList();

        if (File.Exists("tasks.txt"))
        {
            list.LoadFromFile("tasks.txt");
        }

        bool running = true;
        while (running)
        {

            Console.WriteLine("\n=== TODO List ===");
            Console.WriteLine("1.Додати завдання");
            Console.WriteLine("2.Видалити завдання");
            Console.WriteLine("3.Позначити як виконане");
            Console.WriteLine("4.Показати всі завдання");
            Console.WriteLine("5.Показати з категорією");
            Console.WriteLine("6.Вихід");
            Console.Write("Ваш вибір: ");

            // string choice = Console.ReadLine();

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("Невірний вибір, введіть число віж 1 до 6:");
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
                    Category category = Enum.Parse<Category>(Console.ReadLine());
                    Task tasks = new Task(name, description);
                    tasks.SetCategory(category);
                    list.AddTask(tasks);

                    Console.WriteLine("✓ Завдання додано!");
                    break;
                case 2:
                    var allTasks1 = list.GetTask();
                    if (allTasks1.Count > 0)
                    {
                        foreach (var task in allTasks1)
                        {
                            Console.WriteLine($"ID: {task.GetId()} Name: {task.GetName()} Description: {task.GetDescription()} Category: {task.GetCategory()} Status: {task.GetIsCompleted()}");
                        }
                    }
                    Console.WriteLine("Введіть id задачі яку ви хочете видалити");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
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
                    list.RemoveTask(id);
                    
                    
                    Console.WriteLine("Завдання видалено!");
                    break;
                case 3:
                    int idCompleted = 0;
                    var allTasks2 = list.GetTask();
                    if (allTasks2.Count > 0)
                    {
                        foreach (var task in allTasks2)
                        {
                            Console.WriteLine($"ID: {task.GetId()} Name: {task.GetName()} Description: {task.GetDescription()} Category: {task.GetCategory()} Status: {task.GetIsCompleted()}");
                        }
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
                    list.MarkTaskCompleted(idCompleted);

                    break;
                    
                case 4:
                    var res1 = list.GetTask();
                    if (res1.Count > 0)
                    {
                        foreach (var task in res1)
                        {
                            Console.WriteLine($"ID: {task.GetId()} Name: {task.GetName()} Description: {task.GetDescription()} Category: {task.GetCategory()} Status: {task.GetIsCompleted()}");
                        }
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
                        
                    }
                    
                    var res2 = list.GetTaskByCategory(category1);
                    if (res2.Count > 0)
                    {
                        foreach (var task in res2)
                        {
                            Console.WriteLine($"ID: {task.GetId()} Name: {task.GetName()} Description: {task.GetDescription()} Category: {task.GetCategory()} Status: {task.GetIsCompleted()}");
                        }
                    }

                    break;
                case 6:
                    running = false;
                    break;
                default:
                    break;
            }
            list.SaveToFile("tasks.txt");
        }
    }
}