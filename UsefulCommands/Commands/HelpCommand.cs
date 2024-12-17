public class HelpCommand : ICommand
{
    public IReadOnlyCollection<string> PossibleCommands => ["?", "help", "h"];
    
    
    public ExecuteEndResult Execute(ExecuteArgs args)
    {
        Console.WriteLine("HelpList:");
        var commands = args.ProgramInstance.Commands;
        foreach (var c in commands)
        {
            var possibleCommands = c.PossibleCommands;
            if (possibleCommands.Count == 0)
            {
                Console.WriteLine($"A command of type {c.GetType()} appears to have no possible commands.");
                continue;
            }
            
            Console.Write($"{string.Join(", ", possibleCommands)}");
            if (c is IHasHelpText ht) Console.Write($": {ht.HelpText}");
            Console.WriteLine(";");
        }
        
        
        return new ExecuteEndResult();
    }
}