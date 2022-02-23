namespace SampleLibrary;

public class QueryManager
{
    private HashSetOperators hashSetOperators = new HashSetOperators();
    private Queries query;
    public Queries Query
    {
        set
        {
            query = value;
        }
    }

    public HashSet<int> GetFileIdsMatchZeroAndPlusAndMinusQueries
    (Dictionary<string,HashSet<int>> dict)
    {
        var fileIDsMatchZeroQueries = hashSetOperators.And(
            GetHashSetsOfListOfQueries(dict, query.zeroQueries));

        var fileIDsMatchPlusQueries = hashSetOperators.Or(
            GetHashSetsOfListOfQueries(dict, query.plusQueries));

        var fileIDsMatchMinusQueries = hashSetOperators.Or(
            GetHashSetsOfListOfQueries(dict, query.minusQueries));

        return hashSetOperators.Sub(
            GetFileIdsMatchZeroAndPlusQueries
                (fileIDsMatchZeroQueries,fileIDsMatchPlusQueries), 
                    fileIDsMatchMinusQueries);
    }
    private List<HashSet<int>> GetHashSetsOfListOfQueries
        (Dictionary<string,HashSet<int>> _dictionary,List<string> queries)
    {
        return queries.Select(query =>
            {
                if(!_dictionary.ContainsKey(query))
                {
                    return new HashSet<int>();
                }
                return _dictionary[query];

            }).ToList();
    }

    private HashSet<int> GetFileIdsMatchZeroAndPlusQueries
        (HashSet<int>zeroHashSet, HashSet<int>plusHashSet)
    {
        var fileIDsMatchZeroAndPlusQueries = hashSetOperators.And
            (new List<HashSet<int>> {zeroHashSet, plusHashSet});
        
        if (zeroHashSet.Count() == 0 || plusHashSet.Count() == 0)
        {
            fileIDsMatchZeroAndPlusQueries = hashSetOperators.Or
                (new List<HashSet<int>>{zeroHashSet, plusHashSet});
        }
        return fileIDsMatchZeroAndPlusQueries;
    }

}
