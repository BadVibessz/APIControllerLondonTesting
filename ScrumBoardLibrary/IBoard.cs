namespace ScrumBoardLibrary;

public interface IBoard
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<IColumn> Columns { get; set; }
    public void AddColumn(int id, string name);
    public void AddTask(ITask task);
    public void MoveTask(IColumn columnOne, IColumn columnTwo, ITask task);
}