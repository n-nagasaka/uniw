// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic.CompilerServices;

Console.WriteLine("Hello, World!");

var uf = new UniFlags();
uf.Flags.Add(new Flag { ShortName = "f", LongName = "flag1", Description = "flag1" });
uf.Flags.Add(new Flag { ShortName = "g", LongName = "flag2", Description = "flag2" });
Console.WriteLine($"usage={uf.Usage()}");


public interface IOption
{
    bool IsFlag { get; }

    bool Match(string arg);
}

public sealed class Flag : IOption
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

    public bool IsFlag => true;
    public bool Match(string arg)
    {
        if (ShortName != null && arg == $"-{ShortName}")
        {
            return true;
        }
        if (LongName != null && arg == $"--{LongName}")
        {
            return true;
        }

        return false;
    }
}

public sealed class Option : IOption
{
    public Option()
    {
        ValueName = "";
    }

    public Option(string? shortName, string? longName, string valueName, string? description)
    {
        ShortName = shortName;
        LongName = longName;
        ValueName = valueName;
        Description = description;
    }

    public string? ShortName { get; init; }

    public string? LongName { get; init; }

    public string ValueName { get; init; }

    public string? Description { get; init; }

    public bool IsFlag => false;

    public bool Match(string arg)
    {
        if (ShortName != null && arg == $"-{ShortName}")
        {
            return true;
        }
        if (LongName != null && arg == $"--{LongName}")
        {
            return true;
        }

        return false;
    }
}

public class UniFlags
{
    public List<IOption> Flags { get; } = new List<IOption>();

    public static string GetUsageStr(Flag flag)
    {
        if (flag.ShortName != null)
        {
            return $"-{flag.ShortName}";
        }
        if (flag.LongName != null)
        {
            return $"--{flag.LongName}";
        }
        else
        {
            return string.Empty;
        }
    }

    public static string GetUsageStr(Option flag)
    {
        // if (flag.LongName != null && flag.ShortName != null)
        // {
        //     return $"-{flag.ShortName} | --{flag.LongName}";
        // }
        // else if (flag.LongName != null)
        // {
        //     return $"--{flag.LongName}";
        // }
        // else if (flag.ShortName != null)
        // {
        //     return $"-{flag.ShortName}";
        // }
        // else
        // {
        //     return string.Empty;
        // }
        if (flag.ShortName == null) return "";
        return $"[-{flag.ShortName} <{flag.ValueName}>]";
    }

    public static string GetUsageStr(IOption option)
    {
        var result = option switch
        {
            Flag flag => GetUsageStr(flag),
            Option option2 => GetUsageStr(option2),
            _ => string.Empty
        };

        return result;
    }

    public string Usage()

    {   var x = string.Join(", ", Flags.Select(i => $"[{GetUsageStr(i)}]"));
        return "Usage: uniflag " + x;
    }

    public ParseResult Parse(string[] args)
    {
        var d = new Dictionary<int, bool>();

        for (var i = 0; i < Flags.Count; i++)
        {
            if (Flags[i].IsFlag)
            {
                if (args.Any(j => Flags[i].Match(j)))
                {
                    d[i] = true;
                }
                else
                {
                    d[i] = false;
                }
            }
        }

        return new ParseResult
        {
            Args = (from i in args where !i.StartsWith("-") select i).ToArray(),
            Flags = d
        };
    }
}

public class ParseResult
{
    public required string[] Args { get; init; }
    public required Dictionary<int, bool> Flags { get; init; }
}