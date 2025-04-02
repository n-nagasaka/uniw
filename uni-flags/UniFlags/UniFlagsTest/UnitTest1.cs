using Xunit.Abstractions;

namespace UniFlagsTest;

public class UnitTest1(ITestOutputHelper testOutputHelper)
{

    [Fact]
    public void Test2()
    {
        var uf = new UniFlags();
        uf.Flags.Add(new Flag { ShortName = "f", LongName = "flag1", Description = "flag1" });
        uf.Flags.Add(new Flag { ShortName = "g", LongName = "flag2", Description = "flag2" });

        var usage = uf.Usage();

        Assert.Contains("[-f | --flag1]", usage);
        Assert.Contains("[-g | --flag2]", usage);
    }

    [Fact]
    public void Test3()
    {
        var uf = new UniFlags();
        uf.Flags.Add(new Flag { ShortName = "f", Description = "flag1" });
        uf.Flags.Add(new Flag { LongName = "flag2", Description = "flag2" });
        
        var usage = uf.Usage();

        Assert.Contains("[-f]", usage);
        Assert.Contains("[--flag2]", usage);
    }

    [Fact]
    public void Test4()
    {
        
        var uf = new UniFlags();
        uf.Flags.Add(new Flag { ShortName = "f", Description = "flag1" });

        var result = uf.Parse(new string[] {"-f", "abc", "Def"});
        Assert.Equal(new [] { "abc", "Def" }, result.Args);
        Assert.Equal(new [] { true }, result.Flags);
    }
}

