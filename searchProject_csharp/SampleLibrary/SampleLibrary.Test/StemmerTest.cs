using Xunit;

namespace SampleLibrary.Test;

public class StemmerTest
{
    private Stemmer stemmer = new Stemmer();
    [Fact]
    public void test(){
        Assert.Equal("optim", stemmer.StemWord("optimization"));
    }
    [Fact]
    public void endingWithEdTest(){
        Assert.Equal("work", stemmer.StemWord("worked"));
    }
    [Fact]
    public void endingWithConsTest(){
        Assert.Equal("debug", stemmer.StemWord("debugging"));
    }
    [Fact]
    public void pluralWithSTest(){
        Assert.Equal("car", stemmer.StemWord("cars"));
    }
    [Fact]
    public void endingWithAbleTest(){
        Assert.Equal("compat", stemmer.StemWord("compatible"));
    }
    [Fact]
    public void endingWithIzationTest(){
        Assert.Equal("final", stemmer.StemWord("finalization"));
    }
    [Fact]
    public void endingWithAtionAndAtorTest(){
        Assert.Equal("alloc", stemmer.StemWord("allocation"));
        Assert.Equal("motiv", stemmer.StemWord("motivator"));
    }
    [Fact]
    public void endingWithFulnessTest(){
        Assert.Equal("aw", stemmer.StemWord("awfulness"));
    }
    [Fact]
    public void endingWithETest(){
        Assert.Equal("ablaz", stemmer.StemWord("ablaze"));
    }
}