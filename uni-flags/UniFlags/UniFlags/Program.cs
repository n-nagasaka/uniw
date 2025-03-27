// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

class Flag
{
    public string ShortName { get; set; }

    public string LongName { get; set; }

    public string Name { get; }

    public string Description { get; set; }
}

class UniFlag
{
    public List<Flag> Flags { get; set; }

    public string Usage()
    {
        return "Usage: uniflag ";
    }
}