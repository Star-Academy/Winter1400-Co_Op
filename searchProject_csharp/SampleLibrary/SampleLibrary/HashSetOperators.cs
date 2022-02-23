namespace SampleLibrary{

public class HashSetOperators
{
    public HashSet<int> Sub(HashSet<int> hashSet1, HashSet<int> hashSet2)
    {
         return hashSet1.Where(set => !hashSet2.Contains(set)).ToHashSet();
    }

    public HashSet<int> And (List<HashSet<int>> hashSets)
    {

        if (hashSets.Count == 0){
            return new HashSet<int>();
        }
        return hashSets[0].Where
            (element => hashSets.TrueForAll
                (set => set.Contains(element))).ToHashSet();
    }

    public HashSet<int> Or (List<HashSet<int>> hashSets)
    {
        return hashSets.SelectMany(set => set).ToHashSet();
    }
}
}
