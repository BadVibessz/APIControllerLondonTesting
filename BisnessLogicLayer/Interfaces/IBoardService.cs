using BisnessLogicLayer.DTO;

namespace BisnessLogicLayer.Interfaces;

public interface IBoardService
{
    List<BoardDTO> GetAllBoards();
    public void CreateBoard(int id, string name);
    public BoardDTO GetBoard(int id);
    public void RemoveBoard(int id);
    public void AddColumn(int boardId, int columnId, string columnName);
}