import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.HashSet;

public class HashSetOperatorsTest {

    ArrayList<HashSet<Integer>> sets1 = new ArrayList<>();
    ArrayList<HashSet<Integer>> sets2 = new ArrayList<>();
    HashSet<Integer> set1 = new HashSet<>();
    HashSet<Integer> set2 = new HashSet<>();
    HashSet<Integer> set3 = new HashSet<>();
    HashSet<Integer> set4 = new HashSet<>();

    @BeforeEach
    private void initialize(){
        set1.add(1);
        set1.add(3);
        set1.add(2);
        set1.add(4);

        set2.add(1);
        set2.add(3);
        set2.add(5);
        set2.add(6);

        set3.add(1);
        set3.add(3);
        set3.add(9);
        set3.add(7);

        sets1.add(set1);
        sets1.add(set2);
        sets1.add(set3);

        sets2.add(set1);
        sets2.add(set4);
    }

    @Test
    public void testAnd1(){

        HashSet<Integer> expected = new HashSet<>();
        expected.add(1);
        expected.add(3);

        Assertions.assertEquals(HashSetOperators.and(sets1), expected);
    }

    @Test
    public void testAnd2(){
        HashSet<Integer> expected = new HashSet<>();
        Assertions.assertEquals(HashSetOperators.and(sets2), expected);
    }

    @Test
    public void testOr1(){
        HashSet<Integer> expected = new HashSet<>();

        expected.add(1);
        expected.add(2);
        expected.add(3);
        expected.add(4);
        expected.add(5);
        expected.add(6);
        expected.add(7);
        expected.add(9);

        Assertions.assertEquals(HashSetOperators.or(sets1), expected);
    }

    @Test
    public void testOr2(){
        HashSet<Integer> expected = new HashSet<>();
        expected.add(1);
        expected.add(2);
        expected.add(3);
        expected.add(4);

        Assertions.assertEquals(HashSetOperators.or(sets2), expected);
    }

    @Test
    public void testSub1(){
        HashSet<Integer> expected = new HashSet<>();
        expected.add(2);
        expected.add(4);
        Assertions.assertEquals(HashSetOperators.sub(set1,set2), expected);
    }

    @Test
    public void testSub2(){
        Assertions.assertEquals(HashSetOperators.sub(set1,set4), set1);
    }
}
