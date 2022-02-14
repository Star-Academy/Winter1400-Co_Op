namespace SampleLibrary;

public class HashSetOperators
{
    public static HashSet<int> Sub(HashSet<int> hashSet1, HashSet<int> hashSet2){
         return hashSet1.Where(set => !hashSet2.Contains(set)).ToHashSet();
    }

    public static HashSet<int> And (List<HashSet<int>> hashSets){

        if (hashSets.Count == 0){
            return new HashSet<int>();
        }
        return hashSets[0].Where
            (element => hashSets.TrueForAll
                (set => set.Contains(element))).ToHashSet();
    }

    public static HashSet<int> Or (List<HashSet<int>> hashSets){
        return hashSets.SelectMany(set => set).ToHashSet();
    }
}
