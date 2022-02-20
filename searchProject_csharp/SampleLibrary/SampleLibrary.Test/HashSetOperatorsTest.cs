using Xunit;
using System.Collections.Generic;

namespace SampleLibrary.Test;

public class HashSetOperatOrsTest
{
    private readonly List<HashSet<int>> sets1, sets2;
    private readonly HashSet<int> set1, set2, set3, set4;
    private readonly HashSetOperators hashSetOperators;

    public HashSetOperatOrsTest()
    {
        set1 = new HashSet<int>{1, 2, 3, 4};
        set2 = new HashSet<int>{1, 3, 5, 6};
        set3 = new HashSet<int>{1, 3, 9, 7};
        set4 = new HashSet<int>();

        sets1 = new List<HashSet<int>>{set1, set2, set3};
        sets2 = new List<HashSet<int>>{set1, set4};

        hashSetOperators = new();
    }

    [Fact]
    public void TestAnd1(){

        var expected = new HashSet<int>(){1, 3};
        Assert.Equal(hashSetOperators.And(sets1), expected);
    }

    [Fact]
    public void TestAnd2(){
        var expected = new HashSet<int>();
        Assert.Equal(hashSetOperators.And(sets2), expected);
    }

    [Fact]
    public void TestOr1(){
        var expected = new HashSet<int>{1, 2, 3, 4, 5, 6, 7, 9};

        Assert.Equal(hashSetOperators.Or(sets1), expected);
    }

    [Fact]
    public void TestOr2(){
        var expected = new HashSet<int>{1, 2, 3, 4};

        Assert.Equal(hashSetOperators.Or(sets2), expected);
    }

    [Fact]
    public void TestSub1(){
        var expected = new HashSet<int>{2, 4};

        Assert.Equal(hashSetOperators.Sub(set1,set2), expected);
    }

    [Fact]
    public void TestSub2(){
        Assert.Equal(hashSetOperators.Sub(set1,set4), set1);
    }
    
}
