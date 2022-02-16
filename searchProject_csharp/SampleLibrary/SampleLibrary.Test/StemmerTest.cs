using Xunit;

namespace SampleLibrary.Test;

public class StemmerTest
{
    private Stemmer _stemmer = new Stemmer();
    [Fact]
    public void test(){
        Assert.Equal("optim", _stemmer.StemWord("optimization"));
    }
    [Fact]
    public void endingWithEdTest(){
        Assert.Equal("work", _stemmer.StemWord("worked"));
    }
    [Fact]
    public void endingWithConsTest(){
        Assert.Equal("debug", _stemmer.StemWord("debugging"));
    }
    [Fact]
    public void pluralWithSTest(){
        Assert.Equal("car", _stemmer.StemWord("cars"));
    }
    [Fact]
    public void endingWithAbleTest(){
        Assert.Equal("compat", _stemmer.StemWord("compatible"));
    }
    [Fact]
    public void endingWithIzationTest(){
        Assert.Equal("final", _stemmer.StemWord("finalization"));
    }
    [Fact]
    public void endingWithAtionTest(){
        Assert.Equal("alloc", _stemmer.StemWord("allocation"));
    }[Fact]
    public void endingWithAtorTest(){
        Assert.Equal("motiv", _stemmer.StemWord("motivator"));
    }
    [Fact]
    public void endingWithFulnessTest(){
        Assert.Equal("aw", _stemmer.StemWord("awfulness"));
    }
    [Fact]
    public void endingWithETest(){
        Assert.Equal("ablaz", _stemmer.StemWord("ablaze"));
    }
}