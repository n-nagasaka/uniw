namespace UniFlagsTest;

public class UnitTest1
{

    [Fact]
    public void Test2()
    {
        var uf = new UniFlags();
        uf.Flags.Add(new Flag { ShortName = "f", LongName = "flag1", Description = "flag1" });
        uf.Flags.Add(new Flag { ShortName = "g", LongName = "flag2", Description = "flag2" });

        var usage = uf.Usage();
        Assert.Contains("-f", usage);
        Assert.Contains("--flag1", usage);
        Assert.Contains("-g", usage);
        Assert.Contains("--flag2", usage);
    }
}
