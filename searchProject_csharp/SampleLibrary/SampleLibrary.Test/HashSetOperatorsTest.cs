using Xunit;
using System.Collections.Generic;

namespace SampleLibrary.Test;

public class HashSetOperatOrsTest
{

    private List<HashSet<int>> _sets1, _sets2;
    private HashSet<int> _set1, _set2, _set3, _set4;


    public HashSetOperatOrsTest()
    {
        _set1 = new HashSet<int>{1, 2, 3, 4};
        _set2 = new HashSet<int>{1, 3, 5, 6};
        _set3 = new HashSet<int>{1, 3, 9, 7};
        _set4 = new HashSet<int>();

        _sets1 = new List<HashSet<int>>{_set1, _set2, _set3};
        _sets2 = new List<HashSet<int>>{_set1, _set4};
    }

    [Fact]
    public void TestAnd1(){

        var expected = new HashSet<int>(){1, 3};
        Assert.Equal(HashSetOperators.And(_sets1), expected);
    }

    [Fact]
    public void TestAnd2(){
        var expected = new HashSet<int>();
        Assert.Equal(HashSetOperators.And(_sets2), expected);
    }

    [Fact]
    public void TestOr1(){
        var expected = new HashSet<int>{1, 2, 3, 4, 5, 6, 7, 9};

        Assert.Equal(HashSetOperators.Or(_sets1), expected);
    }

    [Fact]
    public void TestOr2(){
        var expected = new HashSet<int>{1, 2, 3, 4};

        Assert.Equal(HashSetOperators.Or(_sets2), expected);
    }

    [Fact]
    public void TestSub1(){
        var expected = new HashSet<int>{2, 4};

        Assert.Equal(HashSetOperators.Sub(_set1,_set2), expected);
    }

    [Fact]
    public void TestSub2(){
        Assert.Equal(HashSetOperators.Sub(_set1,_set4), _set1);
    }
    
}
