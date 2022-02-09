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
        HashSet<Integer> hashSet = (HashSet<Integer>) hashSets.get(0).clone();

        for (int i = 1; i < hashSets.size(); i++){
            hashSet.retainAll(hashSets.get(i));
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
    //[1,2,3,4] [1,2,5] == > [1,2,3,4,5]

}
