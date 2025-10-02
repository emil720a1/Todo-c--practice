namespace To_do;

public class Task
{
    private string Name { get; set; }
    private string Description { get; set; }
    private Category Category { get; set; }
    private bool isCompleted { get; set; }
    private int id { get; set; }

    public Task(string Name, string Description)
    {
        this.Name = Name;
        this.Description = Description;
        isCompleted = false;
    }

    public int GetId()
    {
        return id;
    }

    public void SetId(int id)   
    {
        this.id = id;
    }

    public string GetName()
    {
        return Name;
    }
    
    public string GetDescription()
    {
        return Description;
    }

    public void SetIsCompleted(bool isCompleted)
    {
        this.isCompleted = isCompleted;
    }
    
    public bool GetIsCompleted()
    {
        return isCompleted;
    }
    
    public void SetCategory(Category category)
    {
        Category = category;
    }

    public Category GetCategory()
    {
       return Category;
    }

    public void MarkTaskCompleted()
    {
        isCompleted = true;
    }
    
}