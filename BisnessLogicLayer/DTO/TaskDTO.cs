using ScrumBoardLibrary;

namespace BisnessLogicLayer.DTO;

public class TaskDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }

    public TaskDTO(ITask task)
    {
        Id = task.Id;
        Name = task.Name;
        Description = task.Description;
        Priority = task.Priority;
    }
}