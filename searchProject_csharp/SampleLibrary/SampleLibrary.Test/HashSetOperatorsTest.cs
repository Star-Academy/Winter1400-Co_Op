using Xunit;
using System;
using SampleLibrary;
using System.Collections.Generic;

namespace SampleLibrary.Test
{
    public class HashSetOperatOrsTest
    {

        List<HashSet<int>> sets1 = new ();
        List<HashSet<int>> sets2 = new ();
        HashSet<int> set1 = new ();
        HashSet<int> set2 = new ();
        HashSet<int> set3 = new ();
        HashSet<int> set4 = new ();


        public HashSetOperatOrsTest()
        {
            set1.Add(1);
            set1.Add(3);
            set1.Add(2);
            set1.Add(4);

            set2.Add(1);
            set2.Add(3);
            set2.Add(5);
            set2.Add(6);

            set3.Add(1);
            set3.Add(3);
            set3.Add(9);
            set3.Add(7);

            sets1.Add(set1);
            sets1.Add(set2);
            sets1.Add(set3);

            sets2.Add(set1);
            sets2.Add(set4);
        }

    [Fact]
    public void testAnd1(){

        var expected = new HashSet<int>();
        expected.Add(1);
        expected.Add(3);

        Assert.Equal(HashSetOperators.And(sets1), expected);
    }

    [Fact]
    public void testAnd2(){
        var expected = new HashSet<int>();
        Assert.Equal(HashSetOperators.And(sets2), expected);
    }

    [Fact]
    public void testOr1(){
        var expected = new HashSet<int>();

        expected.Add(1);
        expected.Add(2);
        expected.Add(3);
        expected.Add(4);
        expected.Add(5);
        expected.Add(6);
        expected.Add(7);
        expected.Add(9);

        Assert.Equal(HashSetOperators.Or(sets1), expected);
    }

    [Fact]
    public void testOr2(){
        var expected = new HashSet<int>();
        expected.Add(1);
        expected.Add(2);
        expected.Add(3);
        expected.Add(4);

        Assert.Equal(HashSetOperators.Or(sets2), expected);
    }

    [Fact]
    public void testSub1(){
        var expected = new HashSet<int>();
        expected.Add(2);
        expected.Add(4);
        Assert.Equal(HashSetOperators.Sub(set1,set2), expected);
    }

    [Fact]
    public void testSub2(){
        Assert.Equal(HashSetOperators.Sub(set1,set4), set1);
    }
        
    }
}