using System.ComponentModel.DataAnnotations;

namespace ScrumBoardLibrary;

public class Board
{
    int MAX = 10;

    [Key] public int Id { get; set; }
    public List<Column> Columns { get; set; }

    public string Name { get; set; }

    public Board(int id, string name)
    {
        this.Id = id;
        Name = name;
        Columns = new List<Column>();
    }

    public void AddColumn(int id, string name)
    {
        if (Columns.Count < MAX)
        {
            Column newColumn = new Column(id, name);
            Columns.Add(newColumn);
        }
    }

    public void AddColumn(Column column)
    {
        if (Columns.Count < MAX)
        {
            Columns.Add(column);
            column.Board = this;
        }
    }

    public void AddTask(Task task, Column col = null)
    {
        Column column;
        if (col is null)
            column = Columns[0];
        else column = col;
        column.AddTask(task);
    }

    public void MoveTask(Column columnOne, Column columnTwo, Task task)
    {
        columnOne.DeleteTask(task);
        columnTwo.AddTask(task);
    }
}