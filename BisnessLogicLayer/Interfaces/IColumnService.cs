using BisnessLogicLayer.DTO;

namespace BisnessLogicLayer.Interfaces;

public interface IColumnService
{
    List<ColumnDTO> GetAllColumns();
    public void CreateColumn(int boardId, int id, string name);
    public void UpdateColumn(int id, string newName);
    public ColumnDTO GetColumn(int id);
    public void RemoveColumn(int id);
}