// See https://aka.ms/new-console-template for more information

public sealed class Program
{
    private Program()
    {
        _commands =
        [
            new ExitCommand(),
            new HelpCommand(),
            new LineCountCommand(),
            // Add your commands here manually
        ];
    }
    
    
    public IEnumerable<ICommand> Commands => _commands.AsReadonly();
    private readonly ICollection<ICommand> _commands;
    
    public string? RawInput { get; private set; }
    public string? InputCommand { get; private set; }
    public IReadOnlyList<string> InputArguments { get; private set; } = null!;


    private static void Main()
    {
        var program = new Program();

        while (true)
        {
            Console.Write(">");
            
            program.RawInput = Console.ReadLine()?.Trim();
            {
                var splitInput = program.RawInput?.Split(' ');
                program.InputCommand = splitInput?.FirstOrDefault();
                program.InputArguments = splitInput?.Skip(1).ToList() ?? [];
            }

            
            var fittingCommand = program.Commands
                .FirstOrDefault(c => c.PossibleCommands.Contains(program.InputCommand));

            if (fittingCommand == null)
            {
                Console.WriteLine("Unknown command, use ? for help.");
                continue;
            }

            
            var result = fittingCommand.Execute(new ExecuteArgs { ProgramInstance = program });
            if (result.QuitProgram) break;
        }
    }
}