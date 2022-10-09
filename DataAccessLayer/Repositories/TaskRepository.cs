using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;
using Task = ScrumBoardLibrary.Task;

namespace DataAccessLayer.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly DbContext _db;
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;

    public TaskRepository(DbContext db, IBoardRepository boardRepository, IColumnRepository columnRepository)
    {
        _db = db;
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
    }

    public List<Task> GetAllTasks()
    {
        var tasks = _db.Tasks.ToList();
        return tasks;
    }

    public Task Get(int id)
    {
        return GetAllTasks().Find(t => t.Id == id);
    }

    public void Create(int boardId,int columnId, int id, string name, string desc, int prior)
    {
        var tasks = _db.Tasks;
        var task = new Task(id, name, desc, prior);

        var board = _boardRepository.Get(boardId);
        var col = _columnRepository.Get(columnId);
        
        board.AddTask(task,col);
        tasks.Add(task);

        _db.SaveChanges();
    }

    public void Remove(int boardId, int id)
    {
        var tasks = _db.Tasks;
        var task = GetAllTasks().Find(t => t.Id == id);
        tasks.Remove(task);


        foreach (var col in _boardRepository.Get(boardId).Columns)
        {
            if (col.Tasks.Contains(task))
                col.Tasks.Remove(task);
        }

        _db.SaveChanges();
    }

    public void Update(int taskId, string? newName, string? newDesc, int? newPrior)
    {
        var task = Get(taskId);
        task.Name = newName ?? task.Name;
        task.Description = newDesc ?? task.Description;
        task.Priority = newPrior ?? task.Priority;
        _db.SaveChanges();
    }

    public void Move(int boardId, int taskId, int colToId)
    {
        var task = Get(taskId);
        Remove(boardId, taskId);
        var col = _columnRepository.Get(colToId);
        var board = _boardRepository.Get(boardId);
        board.AddTask(task,col);
        _db.Tasks.Add(task);
        _db.SaveChanges();
    }
}