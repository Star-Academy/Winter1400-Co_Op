using Xunit;
using System.Collections.Generic;
using SampleLibrary;
namespace SampleLibrary.Test;

public class QueriesTest
{
    
    [Fact]
    public void test(){
        Queries queries = new Queries
         ("one two three +four +five -six -seven -eight -nine");
        var expectedZeroQueries = new List<string>();
        expectedZeroQueries.Add("one");
        expectedZeroQueries.Add("two");
        expectedZeroQueries.Add("three");

        var expectedPlusQueries = new List<string>();
        expectedPlusQueries.Add("four");
        expectedPlusQueries.Add("five");

        var expectedMinusQueries = new List<string>();
        expectedMinusQueries.Add("six");
        expectedMinusQueries.Add("seven");
        expectedMinusQueries.Add("eight");
        expectedMinusQueries.Add("nine");

        Assert.Equal(queries.zeroQueries, expectedZeroQueries);
        Assert.Equal(queries.plusQueries, expectedPlusQueries);
        Assert.Equal(queries.minusQueries, expectedMinusQueries);
    }
}
