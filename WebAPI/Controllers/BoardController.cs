using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using BisnessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/boards")]
[ApiController]
public class BoardController : Controller
{
    private readonly IBoardService _boardService;
    private readonly IColumnService? _columnService;

    public BoardController(IBoardService boardService, IColumnService? columnService = null) //, ITaskService taskService)
    {
        _boardService = boardService;
        _columnService = columnService;
    }

    [HttpGet]
    public IActionResult GetAllBoards()
    {
        List<BoardDTO> boards;
        try
        {
            boards = _boardService.GetAllBoards();
        }
        catch (Exception e)
        {
            boards = new List<BoardDTO>();
        }

        return new OkObjectResult(boards);
    }

    [HttpPost]
    public IActionResult CreateBoard(int id, string name)
    {
        try
        {
            _boardService.CreateBoard(id, name);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetBoard(int id)
    {
        BoardDTO board;
        try
        {
            board = _boardService.GetBoard(id);
        }
        catch
        {
            return NotFound();
        }

        return Ok(board);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBoard(int id)
    {
        try
        {
            _boardService.RemoveBoard(id);
        }
        catch
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost("{boardId}/columns")]
    public IActionResult CreateColumn(int boardId, int columnId, string columnName)
    {
        try
        {
            _columnService.CreateColumn(boardId, columnId, columnName);
        }
        catch
        {
            return NotFound("No such board");
        }

        return Ok();
    }


    [HttpDelete("{boardId}/columns/{columnId}")]
    public IActionResult RemoveColumn(int boardId, int columnId)
    {
        try
        {
            _columnService.RemoveColumn(columnId);
        }
        catch
        {
            return NotFound("No such column");
        }

        return Ok();
    }
}