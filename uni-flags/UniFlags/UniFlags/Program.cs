// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class Flag
{
    public string ShortName { get; set; }

    public string LongName { get; set; }

    public string Name { get; }

    public string Description { get; set; }
}

public class UniFlags
{
    public List<Flag> Flags { get; } = new List<Flag>();

    public string Usage()
    {
        return "Usage: uniflag ";
    }
}