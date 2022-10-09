using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;
using Task = ScrumBoardLibrary.Task;

public interface ITaskRepository
{
    List<Task> GetAllTasks();
    Task Get(int id);
    void Create(int boardId,int columnId, int id, string name, string desc, int prior);
    void Remove(int boardId,int id);
    void Update( int taskId, string? newName, string? newDesc, int? newPrior);
    void Move(int boardId, int taskId, int colToId);
}