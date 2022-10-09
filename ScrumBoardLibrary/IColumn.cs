using System.ComponentModel.DataAnnotations;

namespace ScrumBoardLibrary;

public interface IColumn
{
    
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    public void AddTask(Task task);
    public void DeleteTask(Task task);
    public void ChangeName(string name);
    public void Clear();
    public Task GetTask(string name);
    public List<Task> GetAllTasks();
}
