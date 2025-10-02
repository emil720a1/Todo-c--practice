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
            newId = tasks.Max(t => t.GetId()) + 1;
        }
        
        task.SetId(newId);
        tasks.Add(task);
    }

    public void RemoveTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.GetId() == id);
        if (task != null)
        {
            tasks.Remove(task);
        }
    }

    public void MarkTaskCompleted(int id)
    {
        var task = tasks.FirstOrDefault(t => t.GetId() == id);
        if (task != null)
        {
            task.MarkTaskCompleted();
        }
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
           if (task.GetCategory() == category)
           {
               result.Add(task);
           }
       }
       return result;
    }

    public void SaveToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var task in tasks)
            {
                writer.WriteLine($"{task.GetId()};{task.GetName()};{task.GetDescription()};{task.GetCategory()};{task.GetIsCompleted()}");
            }
        };
    }

    public void LoadFromFile(string filePath)
    {
        tasks.Clear();
       using (StreamReader reader = new StreamReader(filePath))
       {
           // List<Task> tasks = new List<Task>();
           string line;
           while ((line = reader.ReadLine()) != null)
           {
              string[] parts = line.Split(";");
              int id = int.Parse(parts[0]);
              string name = parts[1];
              string description = parts[2];
              Category Category = Enum.Parse<Category>(parts[3]);
              bool status = bool.Parse(parts[4]);
             Task task = new Task(name, description);
             task.SetId(id);
             task.SetCategory(Category);
             task.SetIsCompleted(status);
              tasks.Add(task);
           }
       };
    }
    
}