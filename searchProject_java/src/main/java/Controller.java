import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

public class Controller {

    private final HashMap<String, HashSet<Integer>> dictionary;
    private Queries query;

    public Controller(FileReaderInterface fileReader){
        dictionary =  fileReader.getIndexes();
    }

    public void setQuery(Queries query) {
        this.query = query;
    }

    public HashSet<Integer> getFileIdsMatchesZeroAndPlusAndMinusQueries(){

        HashSet<Integer> fileIdsMatchesZeroQueries =
                HashSetOperators.and(getFileIdsOfQueries(query.getZeroQueries()));
        HashSet<Integer> fileIdsMatchesPlusQueries =
                HashSetOperators.or(getFileIdsOfQueries(query.getPlusQueries()));
        HashSet<Integer> fileIdsMatchesMinusQueries =
                HashSetOperators.or(getFileIdsOfQueries(query.getMinusQueries()));

        HashSet<Integer> tempHashSet = getFileIdsMatchesZeroAndPlusQueries(fileIdsMatchesZeroQueries,
                fileIdsMatchesPlusQueries);

        return HashSetOperators.sub(tempHashSet, fileIdsMatchesMinusQueries);
    }

    private ArrayList<HashSet<Integer>> getFileIdsOfQueries(ArrayList<String> queries){

        ArrayList<HashSet<Integer>> hashSetsOfFileIDsForEachQuery = new ArrayList<>();

        for(String query : queries){
            if(dictionary.containsKey(query)){
                hashSetsOfFileIDsForEachQuery.add(dictionary.get(query));
            } else{
                hashSetsOfFileIDsForEachQuery.add(new HashSet<Integer>());
            }
        }
        return hashSetsOfFileIDsForEachQuery;
    }

    private HashSet<Integer> getFileIdsMatchesZeroAndPlusQueries(HashSet<Integer> zeroSet,
                                                                 HashSet<Integer> plusSet){
        if(zeroSet.size() > 0 && plusSet.size() > 0){
            ArrayList<HashSet<Integer>> temp = new ArrayList<>();
            temp.add(zeroSet);
            temp.add(plusSet);
            return HashSetOperators.and(temp);
        }
        return zeroSet.size() == 0 ? plusSet : zeroSet;
    }
}
