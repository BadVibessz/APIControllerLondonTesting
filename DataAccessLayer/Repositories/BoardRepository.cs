using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly DbContext _db;

    public BoardRepository(DbContext db)
    {
        _db = db;
    }

    public List<Board> GetAllBoards()
    {
        var boards = _db.Boards.ToList();
        
        if (boards is null) boards = new List<Board>();
        return boards;
    }

    public Board Get(int id)
    {
        return GetAllBoards().Find(b => b.Id == id);
    }

    public void Create(int id, string name)
    {
        var boards = _db.Boards;
        
        var board = new Board(id, name);
        boards.Add(board);

        _db.SaveChanges();
    }

    public void Remove(int id)
    {
        var boards = _db.Boards;
        var board = Get(id);
        boards.Remove(board);
        _db.SaveChanges();
    }
}