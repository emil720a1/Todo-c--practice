namespace To_do;

public class Task
{
    
    public DateTime? DueDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Category Category { get; set; }
    
    public Priority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public int Id { get; set; }
    

    public Task(string Name, string Description, Priority priority, DateTime? dueDate)
    {
        this.Name = Name;
        this.Description = Description;
        Priority = priority;
        IsCompleted = false;
    }

    public void MarkTaskCompleted()
    {
        IsCompleted = true;
    }

    public override string ToString()
    {
       return $"ID: {Id} {Name} ({Category}), {Priority}, Due: {DueDate?.ToString("yyyy-MM-dd") ?? "No deadline"} - {(IsCompleted ? "Виконано" : DueDate < DateTime.Now ? "Просрочено" : "Не виконано")}";
    }
    
}