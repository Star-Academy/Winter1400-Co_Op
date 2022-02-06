import java.util.ArrayList;
import java.util.HashSet;

public class HashSetOperators {

    public static HashSet<Integer> sub(HashSet<Integer> hashSet1, HashSet<Integer> hashSet2){

        HashSet<Integer> tempAns = (HashSet<Integer>) hashSet1.clone();
        for (int id : hashSet2){
            tempAns.remove(id);
        }
        return tempAns;
    }

    public static HashSet<Integer> and (ArrayList<HashSet<Integer>> hashSets){
        if (hashSets.size() == 0){
            return new HashSet<>();
        }
        HashSet<Integer> hashSet = hashSets.get(0);
        for (int i = 1; i < hashSets.size(); i++){

            var toAnd = hashSets.get(i);
            HashSet<Integer> and = new HashSet<>();
            for (int id : toAnd){
                if (hashSet.contains(id)){
                    and.add(id);
                }
            }
            hashSet = and;
        }
        return hashSet;
    }

    public static HashSet<Integer> or (ArrayList<HashSet<Integer>> hashSets){
        HashSet<Integer> hashSet = new HashSet<>();
        for (HashSet<Integer> tempHashSet : hashSets){
            hashSet.addAll(tempHashSet);
        }
        return hashSet;
    }
}
