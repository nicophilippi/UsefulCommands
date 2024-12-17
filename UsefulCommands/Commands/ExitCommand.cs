public class ExitCommand : ICommand, IHasHelpText
{
    public IReadOnlyCollection<string> PossibleCommands => ["exit", "e"];


    public string HelpText => "Exits the program";

    
    public ExecuteEndResult Execute(ExecuteArgs args)
    {
        return new ExecuteEndResult { QuitProgram = true };
    }

}