import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import javax.swing.*;
import java.util.HashMap;
import java.util.HashSet;

public class ControllerTest {

    HashMap<String, HashSet<Integer>> dictionary = new HashMap<>();
    HashSet<Integer> set1 = new HashSet<>();
    HashSet<Integer> set2 = new HashSet<>();
    HashSet<Integer> set3 = new HashSet<>();
    HashSet<Integer> set4 = new HashSet<>();
    Controller controller = null;

    @BeforeEach
    public void initialize(){

        set1.add(1);
        set1.add(2);
        set1.add(3);
        dictionary.put("one", set1);

        set2.add(1);
        set2.add(4);
        set2.add(5);
        dictionary.put("two", set2);

        set3.add(2);
        set3.add(6);
        set3.add(7);
        dictionary.put("three", set3);

        set4.add(1);
        set4.add(2);
        set4.add(7);
        dictionary.put("four", set4);

        controller = new Controller(new DummyFileReader(dictionary));
    }

    public void checkExpectedAnswer
            (HashSet<Integer> expectedAnswer){
        HashSet<Integer> realAnswer = controller.getFileIdsMatchesZeroAndPlusAndMinusQueries();
        Assertions.assertEquals(expectedAnswer, realAnswer);
    }

    @Test
    public void test1(){
        controller.setQuery(new Queries("one"));

        HashSet<Integer> expectedAnswer = new HashSet<>();
        expectedAnswer.add(1);
        expectedAnswer.add(2);
        expectedAnswer.add(3);

        checkExpectedAnswer(expectedAnswer);

    }

    @Test
    public void test2(){
        controller.setQuery(new Queries("one four"));

        HashSet<Integer> expectedAnswer = new HashSet<>();
        expectedAnswer.add(1);
        expectedAnswer.add(2);

        checkExpectedAnswer(expectedAnswer);

    }

    @Test
    public void test3(){
        controller.setQuery(new Queries("one -three"));

        HashSet<Integer> expectedAnswer = new HashSet<>();
        expectedAnswer.add(1);
        expectedAnswer.add(3);

        checkExpectedAnswer(expectedAnswer);

    }

    @Test
    public void test4(){
        controller.setQuery(new Queries("two five"));

        HashSet<Integer> expectedAnswer = new HashSet<>();

        checkExpectedAnswer(expectedAnswer);

    }

    @Test
    public void test5(){
        controller.setQuery(new Queries("+two +four"));

        HashSet<Integer> expectedAnswer = new HashSet<>();
        expectedAnswer.add(1);
        expectedAnswer.add(2);
        expectedAnswer.add(4);
        expectedAnswer.add(5);
        expectedAnswer.add(7);

        checkExpectedAnswer(expectedAnswer);

    }

    @Test
    public void test6(){
        controller.setQuery(new Queries("+two -four"));

        HashSet<Integer> expectedAnswer = new HashSet<>();

        expectedAnswer.add(4);
        expectedAnswer.add(5);

        checkExpectedAnswer(expectedAnswer);

    }

}
