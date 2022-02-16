import java.util.HashMap;
import java.util.HashSet;

public class DummyFileReader implements FileReaderInterface {

    HashMap<String, HashSet<Integer>> temp;

    public DummyFileReader(HashMap<String, HashSet<Integer>> temp){
        this.temp = temp;
    }

    @Override
    public HashMap<String, HashSet<Integer>> getIndexes() {
        return temp;
    }
}
