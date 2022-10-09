using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;
using ScrumBoardLibrary;

namespace BisnessLogicLayer.Services;

public class ColumnService : IColumnService
{
    private readonly IColumnRepository _repository;

    public ColumnService(IColumnRepository repository)
    {
        this._repository = repository;
    }
    
    public List<ColumnDTO> GetAllColumns()
    {
        var columns = _repository.GetAllColumns();
        var columnsDto = columns.Select(c => new ColumnDTO(c)).ToList();
        return columnsDto;
    }

    public void CreateColumn(int boardId, int id, string name)
    {
        this._repository.Create(boardId, id, name);
    }

    public void UpdateColumn(int id, string newName)
    {
        _repository.Update(id, newName);
    }

    public ColumnDTO GetColumn(int id)
    {
        return new ColumnDTO(_repository.GetAllColumns().Find(c => c.Id == id));
    }

    public void RemoveColumn(int id)
    {
        this._repository.Remove(id);
    }
}