using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public interface IBoardRepository
{
    List<Board> GetAllBoards();
    Board Get(int id);
    void Create(int id, string name);
    void Remove(int id);
}