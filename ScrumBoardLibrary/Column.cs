using System.ComponentModel.DataAnnotations;

namespace ScrumBoardLibrary;

public class Column : IColumn
{
    [Key]  public int Id { get; set; }

    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
    public Board Board { get; set; }

    public Column(int id, string name)
    {
        Id = id;
        Name = name;
        Tasks = new List<Task>();
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void AddTask(Task task)
    {
        Tasks.Add(task);
        task.Column = this;
    }

    public Task GetTask(string name)
    {
        return Tasks.Find(x => x.Name == name);
    }

    public List<Task> GetAllTasks()
    {
        return Tasks;
    }

    public void DeleteTask(Task task)
    {
        Tasks.Remove(task);
    }

    public void Clear()
    {
        Tasks.Clear();
    }

}
