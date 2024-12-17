public class LineCountCommand : ICommand, IHasHelpText
{
    public IReadOnlyCollection<string> PossibleCommands => ["linecount", "lc"];

    public string HelpText => "arguments: \"absolute_root_directory\", [\"expected_file_pattern\"]: Shows the amount of lines in all files below the root. " +
                              "Can optionally scan for specific file patterns (For files with a specific ending, use *.txt)";
    
    public ExecuteEndResult Execute(ExecuteArgs args)
    {
        var inputArgs = args.ProgramInstance.RawInput?.Split("\"");
        if (inputArgs == null || inputArgs.Length == 1 || inputArgs.Length == 2 || inputArgs.Length == 4 || inputArgs.Length > 5)
        {
            Console.WriteLine("Incorrect parameters");
            return new ExecuteEndResult();
        }

        var rootDir = inputArgs[1];
        var filePattern = inputArgs.GetAtOrDefault(3);
        
        Console.WriteLine("Starting Line Counting");
        
        try
        {
            var finalLineCount = GetLineCountInfo(rootDir, filePattern);
            Console.WriteLine($"Total: rawLines={finalLineCount.RawLineCount}, " +
                              $"nonWhiteLines={finalLineCount.NonWhiteLines}, whiteLines={finalLineCount.WhiteLines}," +
                              $"files={finalLineCount.FileCount}, averageLinesPerFile={finalLineCount.AverageLinesPerFile}");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return new ExecuteEndResult();
    }


    private LineCountInfo GetLineCountInfo(string rootDir, string? expectedFilePattern = null)
    {
        return Util.GetFilesBroadSearch(rootDir, expectedFilePattern)
            .Select(GetLineCountInfoOfFile)
            .Aggregate(new LineCountInfo(), (l, r) => l + r);
    }


    private LineCountInfo GetLineCountInfoOfFile(string filePath)
    {
        var content = File.ReadLines(filePath).ToList();
        var o = new LineCountInfo();
        o.RawLineCount = content.Count;
        o.WhiteLines = content.Where(string.IsNullOrWhiteSpace).Count();
        o.FileCount = 1;
        Console.WriteLine($"rawLines={o.RawLineCount}, nonWhiteLines={o.NonWhiteLines}, whiteLines={o.WhiteLines}; in file={filePath}");
        return o;
    }
    
    
    private struct LineCountInfo
    {
        public int RawLineCount;
        public int WhiteLines;
        public int FileCount;
        public int NonWhiteLines => RawLineCount - WhiteLines;
        public int AverageLinesPerFile => RawLineCount / FileCount;


        public static LineCountInfo operator +(LineCountInfo c1, LineCountInfo c2) => new()
        {
            RawLineCount = c1.RawLineCount + c2.RawLineCount,
            WhiteLines = c1.WhiteLines + c2.WhiteLines,
            FileCount = c1.FileCount + c2.FileCount,
        };
    }
}