public interface ICommand
{
    public IReadOnlyCollection<string> PossibleCommands { get; }
    public ExecuteEndResult Execute(ExecuteArgs args);
}