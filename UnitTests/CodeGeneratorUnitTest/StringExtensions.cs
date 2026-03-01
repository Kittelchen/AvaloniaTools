using Common.Extensions;

namespace CodeGeneratorUnitTest;

public class StringExtensionsTests
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void IsEQ_01()
    {
        string s1 = "Test1";
        string s2 = "test1";
        bool b = s1.IsEQ(s2);

        Assert.That(b, Is.True, "IsEQ failed!!!");
    }

    [Test]
    public void IsEQ_02()
    {
        string s1 = "Test1";
        string s2 = "test3";
        
        bool b = s1.IsEQ(s2, true);

        Assert.That(b, Is.False, "IsEQ failed!!!");
    }

    [Test]
    public void IsNumeric_01()
    {
        string s1 = "12.3";

        bool b = s1.IsNumeric();

        Assert.That(b, Is.True, "IsNumeric failed!!!");
    }

    [Test]
    public void IsNumeric_02()
    {
        string s1 = "12,3";

        bool b = s1.IsNumeric();

        Assert.That(b, Is.True, "IsNumeric failed!!!");
    }

    [Test]
    public void IsNumeric_03()
    {
        string s1 = "abc";

        bool b = s1.IsNumeric();

        Assert.That(b, Is.False, "IsNumeric failed!!!");
    }

    [Test]
    public void IsInteger_01()
    {
        string s1 = "12.0";

        bool b = s1.IsInteger();

        Assert.That(b, Is.False, "IsInteger failed!!!");
    }

    [Test]
    public void IsInteger_02()
    {
        string s1 = "12";

        bool b = s1.IsInteger();

        Assert.That(b, Is.True, "IsInteger failed!!!");
    }

    [Test]
    public void StartsWithIgnoreCase_01()
    {
        string s1 = "TETaaaaa";
        bool b = s1.StartsWithIgnoreCase("tet");
        
        Assert.That(b, Is.True, "StartsWithIgnoreCase failed!!!");
    }
    
    [Test]
    public void EndsWithIgnoreCase_01()
    {
        string s1 = "TETAAAAAAA";
        bool b = s1.EndsWithIgnoreCase("aaa");
        
        Assert.That(b, Is.True, "StartsWithIgnoreCase failed!!!");
    }
    
    [Test]
    public void ContainsIgnoreCase_01()
    {
        string s1 = "TETAAAAAAABBBB";
        bool b = s1.ContainsIgnoreCase("aaa");
        
        Assert.That(b, Is.True, "ContainsIgnoreCase failed!!!");
    }
    
    [Test]
    public void IsNull_01()
    {
        string s1 = "";
        
        bool b = s1.IsNullOrEmpty();

        Assert.That(b, Is.True, "IsNull failed!!!");
    }
    
    [Test]
    public void IsNull_02()
    {
        string s1 = string.Empty;
        
        bool b = s1.IsNullOrEmpty();

        Assert.That(b, Is.True, "IsNull failed!!!");
    }
}