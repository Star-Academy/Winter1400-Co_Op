using System.Collections.Generic;

namespace SampleLibrary;

public class Controller
{
    public HashSet<int> Run(string path, string query)
    {
        var dataOfFiles = ReadData(path);
        var indexes = GetIndexes(dataOfFiles);
        var queryManager = new QueryManager(){Query = query};

        return queryManager.GetFileIdsMatchZeroAndPlusAndMinusQueries(indexes);
    }

    public Dictionary<string,string> ReadData(string path)
    {
        return new FileReader(path).GetContentsOfFiles();
    }

    public Dictionary<string, HashSet<int>> GetIndexes
        (Dictionary<string, string> data){

        var contentSaver = new ContentSaver();

        foreach(var item in data)
        {
            var words = ProcessDataAndGiveWords(item.Value);
            contentSaver.StoreWords(words, item.Key);
        }
        
        return contentSaver.Indexes;
    }


    private string[] ProcessDataAndGiveWords(string data)
    {
        try
        {
            return new DocumentProcessor(data).getNormalizedWords();
        }
        catch(DataIsNullException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
}


