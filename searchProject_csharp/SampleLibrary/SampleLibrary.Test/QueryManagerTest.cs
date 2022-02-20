using Xunit;
using System.Collections.Generic;
using Moq;
namespace SampleLibrary.Test;

public class QueryManagerTest
{
    private readonly Dictionary<string, HashSet<int>> dictionary;
    private readonly HashSet<int> set1, set2, set3, set4;
    private readonly QueryManager queryManager;


    public QueryManagerTest(){

        set1 = new (){1, 2, 3};
        set2 = new (){1, 4, 5};
        set3 = new (){2, 6, 7};
        set4 = new (){1, 2, 7};

        dictionary = new(){
            {"one", set1},
            {"two", set2},
            {"three", set3},
            {"four", set4}
        };

       queryManager = new QueryManager();

    }

    public void CheckExpectedAnswer (HashSet<int> expectedAnswer){
        var realAnswer = queryManager.
            GetFileIdsMatchZeroAndPlusAndMinusQueries(dictionary);

        Assert.Equal(expectedAnswer, realAnswer);
    }

    [Fact]
    public void TestZeroQueries1(){
        queryManager.Query = "one";

        var expectedAnswer = new HashSet<int>(){1, 2, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestZeroQueries2(){
        queryManager.Query = "one four";

        var expectedAnswer = new HashSet<int>(){1, 2};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestZeroAndMinusQueries1(){
        queryManager.Query = "one -three";

        var expectedAnswer = new HashSet<int>(){1, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestZeroQueries3(){
        queryManager.Query = "two five";

        var expectedAnswer = new HashSet<int>();

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestPlusQueries(){
        queryManager.Query = "+two +four";

        var expectedAnswer = new HashSet<int>(){1, 2, 4, 5, 7};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestMinusAndPlusQueries(){
        queryManager.Query = "+two -four";

        var expectedAnswer = new HashSet<int>(){4, 5};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void TestZeroAndPlusQueries(){
        queryManager.Query = "one two +four";

        var expectedAnswer = new HashSet<int>(){1};
        
        CheckExpectedAnswer(expectedAnswer);
    }
}
