using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace BisnessLogicLayer.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        this._repository = repository;
    }

    public List<TaskDTO> GetAllTasks()
    {
        var tasks = _repository.GetAllTasks();
        var tasksDto = tasks.Select(c => new TaskDTO(c)).ToList();
        return tasksDto;
    }

    public void CreateTask(int boardId, int columnId, int id, string name, string desc, int prior)
    {
        this._repository.Create(boardId,columnId, id, name, desc, prior);
    }

    public TaskDTO GetTask(int id)
    {
        return new TaskDTO(_repository.GetAllTasks().Find(c => c.Id == id));
    }

    public void RemoveTask(int boardId,int id)
    {
        this._repository.Remove(boardId, id);
    }

    public void UpdateTask( int taskId, string? newName, string? newDesc, int? newPrior)
    {
        _repository.Update( taskId,  newName,  newDesc, newPrior);
    }

    public void MoveTask(int boardId, int taskId, int colToId)
    {
        _repository.Move(boardId, taskId, colToId);
    }
}