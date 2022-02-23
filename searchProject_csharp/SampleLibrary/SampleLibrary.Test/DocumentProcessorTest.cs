using Xunit;
using SampleLibrary;
using System.IO;
using System.Collections.Generic;

namespace SampleLibrary.Test
{
    public class DocumentProcessorTest
    {
         private string foldersPath = @"../../../../../files";
        [Fact]
     public void TestProcessData(){
         string data = "I am looking for publically accessible sources of" + 
             " data depicting braiand neuron functions";
         DocumentProcessor documentProcessor = new DocumentProcessor(data);
         string[] words = documentProcessor.getNormalizedWords();
         string[] expected = new string[]{"i","am","look","for",
             "public","access","sourc","of","data","depict","braiand",
             "neuron","function"};
        Assert.Equal(expected,words);
     }
     [Fact]
     public void TestPuttingWordsInHashMap(){
        Controller controller = new Controller();
        Dictionary<string,string> data =  controller.ReadData(foldersPath);
        Dictionary<string,HashSet<int>> result = controller.GetIndexes(data);
        List<int> list = new List<int>{58913,58569,58578,58886,
            58912,58940,58965,59007,59105,59144,59183};
        HashSet<int> expected = new HashSet<int>(list);
        var y = result["remind"];
        Assert.Equal(expected,y);
    }      
    }
}