namespace UniFlagsTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var a = 0;
        Assert.Equal(1, a);
    }

    [Fact]
    public void Test2()
    {
        var uf = new UniFlags();
    }
}
