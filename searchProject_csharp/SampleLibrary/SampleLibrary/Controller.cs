using System.Collections.Generic;

namespace SampleLibrary;

public class Controller
{
    public Queries Query{set; get;}
    private HashSetOperators _hashSetOperators;
    public Controller(){
        _hashSetOperators = new();
    }

    //"../../../../../files" as input
    public HashSet<int> Run(string path){
        
        FileReader fileReader = new FileReader(path);
        Dictionary<string,string> dataOfFiles =  ReadData(fileReader);
        ContentSaver contentSaver = new ContentSaver();
        foreach(var item in dataOfFiles){
            string[] words = ProcessDataAndGiveWords(item.Value);
            Store(contentSaver, words,item.Key);
        }
        return GetFileIdsMatchZeroAndPlusAndMinusQueries(contentSaver.getIndexes());
    }

    public Dictionary<string,string> ReadData(IFileReader fileReader){
        return fileReader.GetContentsOfFiles();
    }

    public HashSet<int> GetFileIdsMatchZeroAndPlusAndMinusQueries(Dictionary<string,HashSet<int>> dict)
    {

        var fileIDsMatchZeroQueries = _hashSetOperators.And(
            GetHashSetsOfListOfQueries(dict,Query.zeroQueries));

        var fileIDsMatchPlusQueries = _hashSetOperators.Or(
            GetHashSetsOfListOfQueries(dict,Query.plusQueries));

        var fileIDsMatchMinusQueries = _hashSetOperators.Or(
            GetHashSetsOfListOfQueries(dict,Query.minusQueries));


        return _hashSetOperators.Sub(
            GetFileIdsMatchZeroAndPlusQueries
                (fileIDsMatchZeroQueries,fileIDsMatchPlusQueries), 
                    fileIDsMatchMinusQueries);
    }
    private List<HashSet<int>> GetHashSetsOfListOfQueries(Dictionary<string,HashSet<int>> _dictionary,List<string> queries){
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
        var fileIDsMatchZeroAndPlusQueries = _hashSetOperators.And
            (new List<HashSet<int>> {zeroHashSet, plusHashSet});
        
        if (zeroHashSet.Count() == 0 || plusHashSet.Count() == 0)
        {
            fileIDsMatchZeroAndPlusQueries = _hashSetOperators.Or
                (new List<HashSet<int>>{zeroHashSet, plusHashSet});
        }
        return fileIDsMatchZeroAndPlusQueries;
    }
    private string[] ProcessDataAndGiveWords(string data){
        try{
            DocumentProcessor documentProcessor = new DocumentProcessor(data);
            return documentProcessor.getNormalizedWords();
            }
            catch(Exception){
                return null;
            }
    }
    public void Store(ContentSaver contentSaver, string[] words, string fileName){
        contentSaver.StoreWords(words,fileName);
    }       
}
