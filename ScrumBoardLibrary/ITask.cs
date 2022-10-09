namespace ScrumBoardLibrary;

public interface ITask
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public void ChangeName(string name);
    public void ChangePriority(int priority);
    public void ChangeDescription(string descrition);
}
