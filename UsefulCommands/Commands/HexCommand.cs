using System.Text;

public class HexCommand : ICommand, IHasHelpText
{
    public IReadOnlyCollection<string> PossibleCommands => new[]
    {
        "hex",
    };
    
    
    public string HelpText => "Shows the hexadecimal representation of a number along with a way on how to calculate it manually";
    
    
    public ExecuteEndResult Execute(ExecuteArgs args)
    {
        if (args.InputArg(0) == null || !long.TryParse(args.InputArg(0), out var originalValue))
        {
            Console.WriteLine("Need 64-bit Integer as second parameter");
            return new ExecuteEndResult();
        }

        var stringBuilder = new StringBuilder();
        var workingValue = originalValue;

        while (workingValue != 0)
        {
            var mod = workingValue % 16;
            Console.WriteLine($"{workingValue} % 16 = {mod}");
            var newValue = workingValue / 16;
            Console.WriteLine($"{workingValue} / 16 = {newValue}");
            workingValue = newValue;

            char c;
            if (mod == 10) c = 'A';
            else if (mod == 11) c = 'B';
            else if (mod == 12) c = 'C';
            else if (mod == 13) c = 'D';
            else if (mod == 14) c = 'E';
            else if (mod == 15) c = 'F';
            else
            {
                var s = mod.ToString();
                if (s.Length != 1) throw new Exception("SHOULD NOT HAPPEN");
                c = mod.ToString()[0];
            }
            
            stringBuilder.Insert(0, c);
        }

        Console.WriteLine($"{stringBuilder} has been calculated");
        Console.WriteLine($"{originalValue:X} is the actual value");
        return new ExecuteEndResult();
    }
}