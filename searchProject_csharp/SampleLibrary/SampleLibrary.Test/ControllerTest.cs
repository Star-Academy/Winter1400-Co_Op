using Xunit;
using System.Collections.Generic;
using Moq;
namespace SampleLibrary.Test;

public class ControllerTest
{
    private readonly Dictionary<string, HashSet<int>> _dictionary;
    private readonly HashSet<int> _set1, _set2, _set3, _set4;
    private readonly Controller _controller;


    public ControllerTest(){

        _set1 = new (){1, 2, 3};
        _set2 = new (){1, 4, 5};
        _set3 = new (){2, 6, 7};
        _set4 = new (){1, 2, 7};

        _dictionary = new(){
            {"one", _set1},
            {"two", _set2},
            {"three", _set3},
            {"four", _set4}
        };

        var dummyFileReader = new Mock<IFileReader>();
        dummyFileReader.Setup(p => p.GetIndexes()).Returns(_dictionary);
        _controller = new Controller(dummyFileReader.Object);
    }

    public void CheckExpectedAnswer (HashSet<int> expectedAnswer){
        var realAnswer = _controller.
            GetFileIdsMatchZeroAndPlusAndMinusQueries();

        Assert.Equal(expectedAnswer, realAnswer);
    }

    [Fact]
    public void Test1(){
        _controller.Query = new Queries("one");

        var expectedAnswer = new HashSet<int>(){1, 2, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test2(){
        _controller.Query = (new Queries("one four"));

        var expectedAnswer = new HashSet<int>(){1, 2};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test3(){
        _controller.Query = new Queries("one -three");

        var expectedAnswer = new HashSet<int>(){1, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test4(){
        _controller.Query = new Queries("two five");

        var expectedAnswer = new HashSet<int>();

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test5(){
        _controller.Query = new Queries("+two +four");

        var expectedAnswer = new HashSet<int>(){1, 2, 4, 5, 7};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test6(){
        _controller.Query = new Queries("+two -four");

        var expectedAnswer = new HashSet<int>(){4, 5};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test7(){
        _controller.Query = new Queries("one two +four");

        var expectedAnswer = new HashSet<int>(){1};
        
        CheckExpectedAnswer(expectedAnswer);
    }
}
