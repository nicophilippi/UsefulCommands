public struct ExecuteArgs
{
    public Program ProgramInstance;


    public string? InputArg(int index) => ProgramInstance.InputArguments.GetAtOrDefault(index);
}