using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using DataAccessLayer.Repositories;
using ScrumBoardLibrary;

namespace BisnessLogicLayer.Services;

public class BoardService : IBoardService
{
    private readonly IBoardRepository _repository;

    public BoardService(IBoardRepository repository)
    {
        this._repository = repository;
    }


    public List<BoardDTO> GetAllBoards()
    {
        var boards = _repository.GetAllBoards();
        var boardsDto = boards.Select(b => new BoardDTO(b)).ToList();
        return boardsDto;
    }

    public void CreateBoard(int id, string name)
    {
        this._repository.Create(id, name);
    }

    public BoardDTO GetBoard(int id)
    {
        return new BoardDTO(_repository.GetAllBoards().Find(b => b.Id == id));

    }

    public void RemoveBoard(int id)
    {
        this._repository.Remove(id);
    }

    public void AddColumn(int boardId, int columnId, string columnName)
    {
        _repository.Get(boardId).AddColumn(columnId,columnName);
    }
}