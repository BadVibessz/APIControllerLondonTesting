using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly DbContext _db;
    private readonly IBoardRepository _boardRepository;

    public ColumnRepository(DbContext db, IBoardRepository boardRepository)
    {
        _db = db;
        _boardRepository = boardRepository;
    }

    public List<Column> GetAllColumns()
    {
        var columns = _db.Columns.ToList();
        return columns;
    }

    public Column Get(int id)
    {
        return GetAllColumns().Find(c => c.Id == id);
    }

    public void Create(int boardId, int id, string name)
    {
        var columns = _db.Columns;
        var board = _boardRepository.Get(boardId);
        var col = new Column(id, name);
        columns.Add(col);
        board.AddColumn(col);
        _db.SaveChanges();
    }

    public void Remove(int id)
    {
        var columns = _db.Columns;
        columns.Remove( Get(id));
        
        foreach (var board in _db.Boards)
        {
            var col = board.Columns.Find(c => c.Id == id);
            if (col != null)
            {
                board.Columns.Remove(col);
                break;
            }
        }

        _db.SaveChanges();
    }

    public void Update(int id, string newName)
    {
        Get(id).Name = newName;

        foreach (var board in _db.Boards)
        {
            var col = board.Columns.Find(c => c.Id == id);
            if (col is not null)
                col.Name = newName;
        }

        _db.SaveChanges();
    }
}