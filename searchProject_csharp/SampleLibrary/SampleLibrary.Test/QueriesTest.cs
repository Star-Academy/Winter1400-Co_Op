using Xunit;
using System.Collections.Generic;
namespace SampleLibrary.Test;

public class QueriesTest
{

    [Fact]
    public void TestZeroQueries(){

        var queries = new QueriesCreater().CreateQueries("bozorgmehr ali reza +alireza +mohammad -farsh");

        var expectedZeroQueries = new List<string>(){
            "bozorgmehr",
            "ali",
            "reza"
        };

        Assert.Equal(queries.zeroQueries, expectedZeroQueries);
    }

    [Fact]
    public void TestPlusQueries(){

        var queries = new QueriesCreater().CreateQueries
         ("one two three treee +ali +alii +aliii -aliiii");

        var expectedPlusQueries = new List<string>(){
            "ali",
            "alii",
            "aliii"
        };

        Assert.Equal(queries.plusQueries, expectedPlusQueries);
    }

    [Fact]
    public void TestMinusQueries(){

        var queries = new QueriesCreater().CreateQueries
         ("one two three tree +ali +khalafi -farshi -zia");

        var expectedMinusQueries = new List<string>(){
            "farshi",
            "zia"
        };

        Assert.Equal(queries.minusQueries, expectedMinusQueries);
    }
    
    [Fact]
    public void Test(){
        var queries = new QueriesCreater().CreateQueries
            ("one two three +four +five -six -seven -eight -nine");
        
        var expectedZeroQueries = new List<string>()
        {
            "one", "two", "three"
        };
        
        var expectedPlusQueries = new List<string>()
        {
            "four","five"
        };
        
        var expectedMinusQueries = new List<string>()
        {
            "six","seven","eight","nine"
        };
        
        Assert.Equal(queries.zeroQueries, expectedZeroQueries);
        Assert.Equal(queries.plusQueries, expectedPlusQueries);
        Assert.Equal(queries.minusQueries, expectedMinusQueries);
    }
}
