namespace CodeGeneratorUnitTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        
    }

    [Test]
    public void DummyTest01()
    {
        Assert.Pass();
    }
    
    [Test]
    public void DummyTest02()
    {
        Assert.Fail(message: "DummyTest02 failed!!!");
    }
}