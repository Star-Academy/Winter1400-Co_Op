using System.Collections.Generic;

namespace SampleLibrary
{
    public class Controller
    {
        public Queries query{set; get;}
        private Dictionary<string, HashSet<int>> dictionary;
        public Controller(IFileReader fileReader){
            dictionary = fileReader.GetIndexes();
        }

        public HashSet<int> GetFileIdsMatchZeroAndPlusAndMinusQueries()
        {

            var fileIDsMatchZeroQueries = HashSetOperators.And(
                GetHashSetsOfListOfQueries(query.zeroQueries));

            var fileIDsMatchPlusQueries = HashSetOperators.Or(
                GetHashSetsOfListOfQueries(query.plusQueries));

            var fileIDsMatchMinusQueries = HashSetOperators.Or(
                GetHashSetsOfListOfQueries(query.minusQueries));


            return HashSetOperators.Sub(
                GetFileIdsMatchZeroAndPlusQueries
                    (fileIDsMatchZeroQueries,fileIDsMatchPlusQueries), 
                        fileIDsMatchMinusQueries);
        }
        private List<HashSet<int>> GetHashSetsOfListOfQueries(List<string> queries){
            return queries.Select(query =>
                {
                    if(!dictionary.ContainsKey(query))
                    {
                        return new HashSet<int>();
                    }
                    return dictionary[query];

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