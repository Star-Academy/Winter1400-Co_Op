import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import javax.swing.*;
import java.util.HashMap;
import java.util.HashSet;

public class ControllerTest {

    @Test
    public void test(){
        HashMap<String, HashSet<Integer>> dictionary = new HashMap<>();

        HashSet<Integer> set1 = new HashSet<>();
        set1.add(1);
        set1.add(2);
        set1.add(3);
        dictionary.put("one", set1);

        HashSet<Integer> set2 = new HashSet<>();
        set2.add(1);
        set2.add(4);
        set2.add(5);
        dictionary.put("two", set2);

        HashSet<Integer> set3 = new HashSet<>();
        set3.add(2);
        set3.add(6);
        set3.add(7);
        dictionary.put("three", set3);

        HashSet<Integer> set4 = new HashSet<>();
        set4.add(1);
        set4.add(2);
        set4.add(7);
        dictionary.put("four", set4);

        Controller controller = new Controller(new DummyFileReader(dictionary));

        controller.setQuery(new Queries("one"));

        HashSet<Integer> realAnswer = controller.getFileIdsMatchesZeroAndPlusAndMinusQueries();
        HashSet<Integer> expectedAnswer = new HashSet<>();

        expectedAnswer.add(1);
        expectedAnswer.add(2);
        expectedAnswer.add(3);

        Assertions.assertEquals(realAnswer, expectedAnswer);
    }
}
