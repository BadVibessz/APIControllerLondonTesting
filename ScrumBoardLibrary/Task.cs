using System.ComponentModel.DataAnnotations;

namespace ScrumBoardLibrary;

public class Task : ITask
{
    [Key] public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Column Column { get; set; }

    public Task(int id, string name, string description, int priority)
    {
        this.Id = id;
        Name = name;
        Description = description;
        Priority = priority;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeDescription(string description)
    {
        Description = description;
    }

    public void ChangePriority(int priority)
    {
        Priority = priority;
    }
}
