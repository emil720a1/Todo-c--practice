namespace To_do;

public class ToDoList
{
    private List<Task> tasks;
    
    public ToDoList()
    {
        tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        int newId;
        if (tasks.Count == 0)
        {
            newId = 1;
        }
        else
        {
            newId = tasks.Max(t => t.Id) + 1;
        }
        
        task.Id = newId;
        tasks.Add(task);
    }

    public bool RemoveTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            tasks.Remove(task);
            return true;
        }
        return false;
    }

    public bool MarkTaskCompleted(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.MarkTaskCompleted();
            return true;
        }
        return false;
    }
    
    public List<Task> GetTask()
    {
        return tasks;
    }
    
    public List<Task> GetTaskByCategory(Category category)
    {
       List<Task> result = new List<Task>();
       foreach (var task in tasks)
       {
           if (task.Category == category)
           {
               result.Add(task);
           }
       }
       return result;
    }

    public List<Task> GetTaskByPriority(Priority priority)
    {
        List<Task> result = new List<Task>();
        foreach (var task in tasks)
        {
            if (task.Priority == priority)
            {
                result.Add(task);
            }
        }
        return result;
    }

    public bool SaveToFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.Id};{task.Name};{task.Description};{task.Category};{task.IsCompleted};{task.Priority}");
            }
        };
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }

    public bool LoadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            tasks.Clear();
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(";");
                        if (parts.Length != 7)
                        {
                            continue;
                        }
                        int id;
                        if (int.TryParse(parts[0], out id))
                        {
                        }
                        else
                        {
                            Console.WriteLine("Помилка, порожній id");
                            continue;
                        }

                        string name = parts[1];
                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Помилка, ім'я порожнє");
                            continue;
                        }

                        string description = parts[2];

                        if (string.IsNullOrWhiteSpace(description))
                        {
                            Console.WriteLine("Помилка, опис порожній");
                            continue;
                        }

                        Category category;
                        if (Enum.TryParse<Category>(parts[3], out category))
                        {
                        }
                        else
                        {
                            Console.WriteLine("Проблеми з парсингом категорії");
                            continue;
                        }

                        bool status;

                        if (bool.TryParse(parts[4], out status))
                        {
                            Console.WriteLine($"Успішний парсинг: {status}");
                        }
                        else
                        {
                            Console.WriteLine("Проблема з парсингом статусу");
                            continue;
                        }

                        Priority priority;
                        if (!Enum.TryParse<Priority>(parts[5], out priority))
                        {
                            priority = Priority.Low;
                        }

                        DateTime? dueDate = null;
                        if (!string.IsNullOrWhiteSpace(parts[6]))
                        {
                            if (DateTime.TryParse(parts[6], out DateTime parsedDate))
                            {
                                dueDate = parsedDate;
                            }
                        }

                        Task task = new Task(name, description, priority, dueDate);
                        task.Id = id;
                        task.Category = category;
                        task.IsCompleted = status;
                        task.DueDate = dueDate;
                        tasks.Add(task);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Помилка завантаження файлу: {ex.Message}");
                return false;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Несподівана помилка завантаження: {ex.Message}");
                return false;
            }

            return true;
        }
        else
        {
            Console.WriteLine("Файл не існує, починаємо з порожнім списком");
            return true;
        }
    }
}