import java.util.ArrayList;

public class Queries {

    private final String mainQuery;
    private final String splitter = " ";
    private final ArrayList<String> zeroQueries = new ArrayList<>();
    private final ArrayList<String> plusQueries = new ArrayList<>();
    private final ArrayList<String> minusQueries = new ArrayList<>();


    public Queries(String mainQuery){
        this.mainQuery = mainQuery;
        initializeQueriesFromMainQuery();
    }

    private void initializeQueriesFromMainQuery(){
        for (String query : mainQuery.split(splitter)){
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
