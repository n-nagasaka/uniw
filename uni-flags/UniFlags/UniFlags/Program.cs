// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic.CompilerServices;

Console.WriteLine("Hello, World!");

var uf = new UniFlags();
uf.Flags.Add(new Flag { ShortName = "f", LongName = "flag1", Description = "flag1" });
uf.Flags.Add(new Flag { ShortName = "g", LongName = "flag2", Description = "flag2" });
Console.WriteLine($"usage={uf.Usage()}");

public class Flag
{
    public Flag()
    {
    }

    public Flag(string? shortName, string? longName, string? description)
    {
        ShortName = shortName;
        LongName = longName;
        Description = description;
    }

    public string? ShortName { get; init; }

    public string? LongName { get; init; }

    public string? Description { get; init; }
}

public class UniFlags
{
    public List<Flag> Flags { get; } = new List<Flag>();

    public static string GetUsageStr(Flag flag)
    {
        if (flag.LongName != null && flag.ShortName != null)
        {
            return $"-{flag.ShortName} | --{flag.LongName}";
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