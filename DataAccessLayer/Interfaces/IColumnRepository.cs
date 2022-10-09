using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public interface IColumnRepository
{
    List<Column> GetAllColumns();
    Column Get(int id);
    void Create(int boardId, int id, string name);
    void Remove(int id);
    void Update(int id, string newName);
}