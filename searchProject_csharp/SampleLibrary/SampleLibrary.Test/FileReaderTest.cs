using Xunit;
using SampleLibrary;
using System.IO;
using System.Collections.Generic;
namespace SampleLibrary.Test;


public class FileReaderTest
{
    //Files (in local) are in Winter1400-Co_Op/searchProject_csharp/files
    private string foldersPath = @"../../../../../files";
    
    [Fact]
    public void TestImportFiles()
    {
      var fileReader = new FileReader(foldersPath);
      var addresses =  fileReader.getAddressOfFiles();

      Assert.Equal(1000,addresses.Length);          
    }

    [Fact]
    public void TestReadingFile(){
        var fileReader = new FileReader(foldersPath);
        var fileInfos = fileReader.getAddressOfFiles();

        Assert.Equal(Resources.String1, fileReader.ReadDataFromFile(
            fileInfos[0].FullName));
    }

    [Fact]
    public void TestReadingFile2(){
        var fileReader = new FileReader(foldersPath);
        var falsePath = $"{foldersPath}/57111";
        Assert.Null(fileReader.ReadDataFromFile(falsePath));
    }

     [Fact]
     public void TestProcessData(){
         var data = "I am looking for publically accessible sources of" + 
             " data depicting braiand neuron functions.";
         var documentProcessor = new DocumentProcessor(data);
         var words = documentProcessor.getNormalizedWords();
         var expected = new string[]{"i","am","look","for",
             "public","access","sourc","of","data","depict","braiand",
             "neuron","function"};
         for(int i = 0; i < expected.Length;i++){
             Assert.Equal(expected[i],words[i]);
         }
     }

    [Fact]
    public void TestPuttingWordsInHashMap(){
        var controller = new Controller();
        var data =  controller.ReadData(foldersPath);
        var result = controller.GetIndexes(data);
        var list = new List<int>{58913,58569,58578,58886,
            58912,58940,58965,59007,59105,59144,59183};
        var expected = new HashSet<int>(list);
        var x = result.TryGetValue("remind",out HashSet<int> y);
        Assert.Equal(expected,y);
    }

}