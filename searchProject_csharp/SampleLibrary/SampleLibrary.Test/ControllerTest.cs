using Xunit;
using System.Collections.Generic;
namespace SampleLibrary.Test;

public class ControllerTest
    {
    private Dictionary<string, HashSet<int>> dictionary;
    private HashSet<int> set1, set2, set3, set4;
    private Controller controller;


    public ControllerTest(){

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

        controller = new Controller
            (new DummyFileReader(){TempDictionary = dictionary});
    }

    public void CheckExpectedAnswer (HashSet<int> expectedAnswer){
        var realAnswer = controller.
            GetFileIdsMatchZeroAndPlusAndMinusQueries();

        Assert.Equal(expectedAnswer, realAnswer);
    }

[Fact]
public void Test1(){
    controller.Query = new Queries("one");

    var expectedAnswer = new HashSet<int>(){1, 2, 3};

    CheckExpectedAnswer(expectedAnswer);
}

    [Fact]
    public void Test2(){
        controller.Query = (new Queries("one four"));

        var expectedAnswer = new HashSet<int>(){1, 2};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test3(){
        controller.Query = new Queries("one -three");

        var expectedAnswer = new HashSet<int>(){1, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test4(){
        controller.Query = new Queries("two five");

        var expectedAnswer = new HashSet<int>();

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test5(){
        controller.Query = new Queries("+two +four");

        var expectedAnswer = new HashSet<int>(){1, 2, 4, 5, 7};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test6(){
        controller.Query = new Queries("+two -four");

        var expectedAnswer = new HashSet<int>(){4, 5};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test7(){
        controller.Query = new Queries("one two +four");

        var expectedAnswer = new HashSet<int>(){1};
        
        CheckExpectedAnswer(expectedAnswer);
    }
}
