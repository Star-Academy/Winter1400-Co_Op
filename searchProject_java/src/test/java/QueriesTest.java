import org.junit.jupiter.api.Assertions;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;

public class QueriesTest {

    @Test
    public void test(){
        Queries queries = new Queries("one two three +four +five -six -seven -eight -nine");
        ArrayList<String> expectedZeroQueries = new ArrayList<>();
        expectedZeroQueries.add("one");
        expectedZeroQueries.add("two");
        expectedZeroQueries.add("three");

        ArrayList<String> expectedPlusQueries = new ArrayList<>();
        expectedPlusQueries.add("four");
        expectedPlusQueries.add("five");

        ArrayList<String> expectedMinusQueries = new ArrayList<>();
        expectedMinusQueries.add("six");
        expectedMinusQueries.add("seven");
        expectedMinusQueries.add("eight");
        expectedMinusQueries.add("nine");

        Assertions.assertEquals(queries.getZeroQueries(), expectedZeroQueries);
        Assertions.assertEquals(queries.getPlusQueries(), expectedPlusQueries);
        Assertions.assertEquals(queries.getMinusQueries(), expectedMinusQueries);
    }
}
