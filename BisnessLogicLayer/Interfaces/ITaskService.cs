using BisnessLogicLayer.DTO;

namespace BisnessLogicLayer.Interfaces;

public interface ITaskService
{
    List<TaskDTO> GetAllTasks();
    public void CreateTask(int boardId, int columnId, int id, string name,string desc, int prior);
    public TaskDTO GetTask(int id);
    public void RemoveTask(int boardId, int id);
    void UpdateTask(int taskId, string? newName, string? newDesc, int? newPrior);
    void MoveTask(int boardId, int taskId, int colToId);
}