using System.Collections.Generic;

namespace SampleLibrary
{
    public class Controller
    {
        public Queries Query{set; get;}
        private Dictionary<string, HashSet<int>> _dictionary;
        public Controller(IFileReader fileReader){
            _dictionary = fileReader.GetIndexes();
        }

        public HashSet<int> GetFileIdsMatchZeroAndPlusAndMinusQueries()
        {

            var fileIDsMatchZeroQueries = HashSetOperators.And(
                GetHashSetsOfListOfQueries(Query.zeroQueries));

            var fileIDsMatchPlusQueries = HashSetOperators.Or(
                GetHashSetsOfListOfQueries(Query.plusQueries));

            var fileIDsMatchMinusQueries = HashSetOperators.Or(
                GetHashSetsOfListOfQueries(Query.minusQueries));


            return HashSetOperators.Sub(
                GetFileIdsMatchZeroAndPlusQueries
                    (fileIDsMatchZeroQueries,fileIDsMatchPlusQueries), 
                        fileIDsMatchMinusQueries);
        }
        private List<HashSet<int>> GetHashSetsOfListOfQueries(List<string> queries){
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
            var fileIDsMatchZeroAndPlusQueries = HashSetOperators.And
             (new List<HashSet<int>> {zeroHashSet, plusHashSet});
            
            if (zeroHashSet.Count() == 0 || plusHashSet.Count() == 0)
            {
                fileIDsMatchZeroAndPlusQueries = HashSetOperators.Or
                 (new List<HashSet<int>>{zeroHashSet, plusHashSet});
            }
            return fileIDsMatchZeroAndPlusQueries;
        }
    }
}