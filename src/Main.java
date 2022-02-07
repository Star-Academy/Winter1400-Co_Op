import java.util.*;

public class Main {

    private static HashMap<String, HashSet<Integer>> dictionary;
    private static queries query;

    private static ArrayList<HashSet<Integer>> getHashSetsOfQueries(ArrayList<String> queries){

        ArrayList<HashSet<Integer>> hashSets = new ArrayList<>();

        for(String query : queries){
            if(dictionary.containsKey(query)){
                hashSets.add(dictionary.get(query));
            }
        }
        return hashSets;
    }

    private static void scanQuery(){
        System.out.println("Enter the query:");
        Scanner scanner = new Scanner(System.in);
        query = new queries(scanner.nextLine());
    }

    private static HashSet<Integer> getAns(){

        HashSet<Integer> fileIdsForZeroQueries =
                HashSetOperators.and(getHashSetsOfQueries(query.getZeroQueries()));
        HashSet<Integer> fileIdsForPlusQueries =
                HashSetOperators.or(getHashSetsOfQueries(query.getPlusQueries()));
        HashSet<Integer> fileIdsForMinusQueries =
                HashSetOperators.or(getHashSetsOfQueries(query.getMinusQueries()));

        HashSet<Integer> tempHashSet = getHashSetsForZeroAndPlusQueries(fileIdsForZeroQueries,
                fileIdsForPlusQueries);

        return HashSetOperators.sub(tempHashSet, fileIdsForMinusQueries);
    }

    private static HashSet<Integer> getHashSetsForZeroAndPlusQueries(HashSet<Integer> zeroSet,
                                                                    HashSet<Integer> plusSet){
        if(plusSet.size() > 0){
            ArrayList<HashSet<Integer>> temp = new ArrayList<>();
            temp.add(zeroSet);
            temp.add(plusSet);
            return HashSetOperators.and(temp);
        }
        return zeroSet;
    }

    public static void main(String[] args) {
        dictionary =  FileReader.getInstance().fillIndexes();
        scanQuery();
        System.out.println(getAns());
    }
}

class queries{

    private final String mainQuery;
    private final ArrayList<String> zeroQueries = new ArrayList<>();
    private final ArrayList<String> plusQueries = new ArrayList<>();
    private final ArrayList<String> minusQueries = new ArrayList<>();

    public queries(String mainQuery){
        this.mainQuery = mainQuery;
        initializeQueriesFromMainQuery();
    }

    private void initializeQueriesFromMainQuery(){
        for (String query : mainQuery.split(" ")){
            if(query.startsWith("+")){
                plusQueries.add(query.substring(1));
            } else if ( query.startsWith("-")){
                minusQueries.add(query.substring(1));
            } else {
                zeroQueries.add(query);
            }
        }
    }

    public ArrayList<String> getZeroQueries() {
        return zeroQueries;
    }

    public ArrayList<String> getPlusQueries() {
        return plusQueries;
    }

    public ArrayList<String> getMinusQueries() {
        return minusQueries;
    }


}