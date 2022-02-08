import java.util.*;

public class Main {
    public static HashSet<Integer> sub(HashSet<Integer> hashSet1, HashSet<Integer> hashSet2) {
        HashSet<Integer> tempAns = (HashSet<Integer>) hashSet1.clone();
        for (int id : hashSet2) {
            tempAns.remove(id);
        }
        return tempAns;
    }

    public static HashSet<Integer> and(ArrayList<HashSet<Integer>> hashSets) {
        if (hashSets.size() == 0) {
            return new HashSet<>();
        }
        HashSet<Integer> hashSet = hashSets.get(0);
        for (int i = 1; i < hashSets.size(); i++) {

            var toAnd = hashSets.get(i);
            HashSet<Integer> and = new HashSet<>();
            for (int id : toAnd) {
                if (hashSet.contains(id)) {
                    and.add(id);
                }
            }
            hashSet = and;
        }
        return hashSet;
    }


    public static HashSet<Integer> or(ArrayList<HashSet<Integer>> hashSets) {
        HashSet<Integer> hashSet = new HashSet<>();
        for (HashSet<Integer> tempHashSet : hashSets) {
            hashSet.addAll(tempHashSet);
        }
        return hashSet;
    }


    public static ArrayList<HashSet<Integer>> getHashSetsOfQueries(ArrayList<String> queries, HashMap<String, HashSet<Integer>> dictionary) {
        ArrayList<HashSet<Integer>> hashSets = new ArrayList<>();

        for (String query : queries) {
            if (dictionary.containsKey(query)) {
                hashSets.add(dictionary.get(query));
            }
        }
        return hashSets;
    }

    public static void main(String[] args) {
        ArrayList<String> queries0 = new ArrayList<>();
        ArrayList<String> queries1 = new ArrayList<>();
        ArrayList<String> queries2 = new ArrayList<>();
        FileReader fileReader = new FileReader();
        fileReader.fillIndexes();
        System.out.println("Enter the query:");
        Scanner scanner = new Scanner(System.in);
        String input = scanner.nextLine();
        for (String query : input.split(" ")) {
            if (query.startsWith("+")) {
                queries1.add(query.substring(1));
            } else if (query.startsWith("-")) {
                queries2.add(query.substring(1));
            } else {
                queries0.add(query);
            }
        }

        HashSet<Integer> hashSet0 = and(getHashSetsOfQueries(queries0, fileReader.indexes));
        HashSet<Integer> hashSet1 = or(getHashSetsOfQueries(queries1, fileReader.indexes));
        HashSet<Integer> hashSet2 = or(getHashSetsOfQueries(queries2, fileReader.indexes));
        HashSet<Integer> hashSet3 = hashSet0;

        if (hashSet1.size() > 0) {
            ArrayList<HashSet<Integer>> temp = new ArrayList<>();
            temp.add(hashSet0);
            temp.add(hashSet1);
            hashSet3 = and(temp);
        }

        HashSet<Integer> ans = sub(hashSet3, hashSet2);

        System.out.println(ans);

    }
}
