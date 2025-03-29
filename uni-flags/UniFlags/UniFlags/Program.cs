// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class Flag
{
    public string? ShortName { get; set; }

    public string? LongName { get; set; }

    public string? Name { get; }

    public string? Description { get; set; }
}

public class UniFlags
{
    public List<Flag> Flags { get; } = new List<Flag>();

    public static string GetUsageStr(Flag flag)
    {
        if (flag.LongName != null && flag.ShortName != null)
        {
            return $"-{flag.ShortName}|--{flag.LongName}";
        }
        else if (flag.LongName != null)
        {
            return $"--{flag.LongName}";
        }
        else if (flag.ShortName != null)
        {
            return $"-{flag.ShortName}";
        }
        else
        {
            return string.Empty;
        }
    }

    public string Usage()

    {   var x = string.Join(", ", Flags.Select(i => $"[{GetUsageStr(i)}]"));
        return "Usage: uniflag " + x;
    }
}