using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ScrumBoardLibrary;
using WebAPI.Controllers;

namespace LondonTesting;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Should_Return_Board_When_Found()
    {
        //Arrange
        var board = new BoardDTO(new Board(1, "board"));

        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.GetBoard(1))
            .Returns(board);

        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.GetBoard(1);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo(board));
    }

    [Test]
    public void Should_Return_404_When_Board_Not_Found()
    {
        //Arrange
        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.GetBoard(1))
            .Throws(new Exception("Not Found"));

        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.GetBoard(1);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundResult)));
    }

    [Test]
    public void Should_Return_AllBoard_When_Found()
    {
        //Arrange
        var boards = new List<BoardDTO>()
        {
            new(new Board(1, "board1")),
            new(new Board(2, "board2")),
            new(new Board(3, "board3"))
        };

        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.GetAllBoards())
            .Returns(boards);

        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.GetAllBoards();

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
        Assert.That(((OkObjectResult)result).Value, Is.EqualTo(boards));
    }

    [Test]
    public void Should_Return_EmptyList_When_No_Boards_Found()
    {
        //Arrange

        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.GetAllBoards())
            .Throws(new Exception("No boards"));

        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.GetAllBoards();

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkObjectResult)));
        Assert.That((((List<BoardDTO>)((OkObjectResult)result).Value).Count == 0));
    }

    [Test]
    public void Should_Return_Ok_When_Creating_Valid_Board()
    {
        //Arrange
        var board = new BoardDTO(new Board(1, "board"));

        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.CreateBoard(board.Id, board.Name));
        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.CreateBoard(board.Id, board.Name);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
    }

    [Test]
    public void Should_Return_BadRequest_When_Creating_Invalid_Board()
    {
        //Arrange
        var board = new BoardDTO(new Board(-1, "board"));

        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.CreateBoard(board.Id, board.Name))
            .Throws(new Exception("Id cannot be negative int"));
        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.CreateBoard(board.Id, board.Name);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(BadRequestResult)));
    }

    [Test]
    public void Should_Return_Ok_When_Deleting_Existing_Board()
    {
        //Arrange
        var board = new BoardDTO(new Board(1, "board"));

        var mockService = new Mock<IBoardService>();
        // mockService
        //     .Setup(serivce => serivce.CreateBoard(board.Id, board.Name));
        mockService
            .Setup(service => service.RemoveBoard(board.Id));
        var controller = new BoardController(mockService.Object);

        //Act
        controller.CreateBoard(board.Id, board.Name);
        var result = controller.DeleteBoard(board.Id);


        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
    }

    [Test]
    public void Should_Return_NotFound_When_Deleting_Not_Existing_Board()
    {
        //Arrange
        int id = 1;
        var mockService = new Mock<IBoardService>();
        mockService
            .Setup(serivce => serivce.RemoveBoard(id))
            .Throws(new Exception("Deleting not existing board"));

        var controller = new BoardController(mockService.Object);

        //Act
        var result = controller.DeleteBoard(id);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundResult)));
    }

    [Test]
    public void Should_Return_Ok_When_Creating_Valid_Column()
    {
        //Arrange
        var mockColumnService = new Mock<IColumnService>();
        var mockBoardService = new Mock<IBoardService>();

        int boardId = 1, columnId = 1;
        string columnName = "column";

        mockColumnService
            .Setup(service => service.CreateColumn(boardId, columnId, columnName));

        var controller = new BoardController(mockBoardService.Object, mockColumnService.Object);

        //Act
        var result = controller.CreateColumn(boardId, columnId, columnName);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
    }

    [Test]
    public void Should_Return_NotFound_When_Creating_Invalid_Column()
    {
        //Arrange
        var mockColumnService = new Mock<IColumnService>();
        var mockBoardService = new Mock<IBoardService>();

        int boardId = -1, columnId = 1;
        string columnName = "column";

        mockColumnService
            .Setup(service => service.CreateColumn(boardId, columnId, columnName))
            .Throws(new Exception("Invalid board id"));

        var controller = new BoardController(mockBoardService.Object, mockColumnService.Object);

        //Act
        var result = controller.CreateColumn(boardId, columnId, columnName);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundObjectResult)));
    }

    [Test]
    public void Should_Return_Ok_When_Removing_Existing_Column()
    {
        //Arrange
        var mockColumnService = new Mock<IColumnService>();
        var mockBoardService = new Mock<IBoardService>();

        int boardId = 1, columnId = 1;

        mockColumnService
            .Setup(service => service.RemoveColumn(columnId));

        var controller = new BoardController(mockBoardService.Object, mockColumnService.Object);

        //Act
        var result = controller.RemoveColumn(boardId, columnId);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(OkResult)));
    }

    [Test]
    public void Should_Return_NotFound_When_Removing_Not_Existing_Column()
    {
        //Arrange
        var mockColumnService = new Mock<IColumnService>();
        var mockBoardService = new Mock<IBoardService>();

        int boardId = 1, columnId = 1;

        mockColumnService
            .Setup(service => service.RemoveColumn(columnId))
            .Throws(new Exception("No such column"));

        var controller = new BoardController(mockBoardService.Object, mockColumnService.Object);

        //Act
        var result = controller.RemoveColumn(boardId, columnId);

        //Assert
        Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundObjectResult)));
    }
}