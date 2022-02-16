using Xunit;
using System.Collections.Generic;
namespace SampleLibrary.Test;

public class ControllerTest
    {
    Dictionary<string, HashSet<int>> dictionary = new ();
    HashSet<int> set1, set2, set3, set4;
    Controller controller;


    public ControllerTest(){

        set1 = new HashSet<int>(){1, 2, 3};
        set2 = new HashSet<int>(){1, 4, 5};
        set3 = new HashSet<int>(){2, 6, 7};
        set4 = new HashSet<int>(){1, 2, 7};

        dictionary.Add("one", set1);
        dictionary.Add("two", set2);
        dictionary.Add("three", set3);
        dictionary.Add("four", set4);

        controller = new Controller
            (new DummyFileReader(){tempDictionary = dictionary});
    }

    public void CheckExpectedAnswer (HashSet<int> expectedAnswer){
        HashSet<int> realAnswer = controller.
            GetFileIdsMatchZeroAndPlusAndMinusQueries();

        Assert.Equal(expectedAnswer, realAnswer);
    }

[Fact]
public void Test1(){
    controller.query = new Queries("one");

    var expectedAnswer = new HashSet<int>(){1, 2, 3};

    CheckExpectedAnswer(expectedAnswer);
}

    [Fact]
    public void Test2(){
        controller.query = (new Queries("one four"));

        var expectedAnswer = new HashSet<int>(){1, 2};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test3(){
        controller.query = new Queries("one -three");

        var expectedAnswer = new HashSet<int>(){1, 3};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test4(){
        controller.query = new Queries("two five");

        var expectedAnswer = new HashSet<int>();

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test5(){
        controller.query = new Queries("+two +four");

        var expectedAnswer = new HashSet<int>(){1, 2, 4, 5, 7};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test6(){
        controller.query = new Queries("+two -four");

        var expectedAnswer = new HashSet<int>(){4, 5};

        CheckExpectedAnswer(expectedAnswer);
    }

    [Fact]
    public void Test7(){
        controller.query = new Queries("one two +four");

        var expectedAnswer = new HashSet<int>(){1};
        
        CheckExpectedAnswer(expectedAnswer);
    }
}
