using ScrumBoardLibrary;

namespace BisnessLogicLayer.DTO;

public class ColumnDTO
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<ITask> Tasks { get; set; }

    public ColumnDTO(IColumn column)
    {
        Id = column.Id;
        Name = column.Name;

        Tasks = new List<ITask>(column.GetAllTasks());
    }
}