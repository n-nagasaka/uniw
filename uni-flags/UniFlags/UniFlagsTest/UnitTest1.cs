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
        // testOutputHelper.WriteLine(usage);

        Assert.Contains("[-f | --flag1]", usage);
        Assert.Contains("[-g | --flag2]", usage);
    }
}
