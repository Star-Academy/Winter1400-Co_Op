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
        string expected = "I have a 42 yr old male friend, misdiagnosed as havin osteopporosis for two years, who recently found out that hi illness is the rare Gaucher's disease.Gaucher's disease symptoms include: brittle bones (he lost 9 inches off his hieght); enlarged liver and spleen; interna bleeding; and fatigue (all the time). The problem (in Type 1) i attributed to a genetic mutation where there is a lack of th enzyme glucocerebroside in macrophages so the cells swell up This will eventually cause deathEnyzme replacement therapy has been successfully developed an approved by the FDA in the last few years so that those patient administered with this drug (called Ceredase) report a remarkabl improvement in their condition. Ceredase, which is manufacture by biotech biggy company--Genzyme--costs the patient $380,00 per year. Gaucher\\'s disease has justifyably been called \"the mos expensive disease in the world\"NEED INFOI have researched Gaucher's disease at the library but am relyin on netlanders to provide me with any additional information**news, stories, report**people you know with this diseas**ideas, articles about Genzyme Corp, how to get a hold o   enough money to buy some, programs available to help wit   costs**Basically ANY HELP YOU CAN OFFEThanks so very muchDeborah";

        Assert.Equal(expected,fileReader.readDataFromFile(
            fileInfos[0].FullName,fileInfos[0].Name));
    }

    [Fact]
    public void TestReadingFile2(){
        var fileReader = new FileReader(foldersPath);
        string falsePath = $"{foldersPath}/57111";
        string falseName = "57111";
        Assert.Null(fileReader.readDataFromFile(falsePath,falseName));
    }

    // [Fact]
    // public void TestProcessData(){
    //     string data = "I am looking for publically accessible sources of" + 
    //         " data depicting braiand neuron functions.";

    //     var contentSaver = new ContentSaver();
        
    //     var result = fileReader.processDataAndGiveWords(data);
    //     string[] expected = new string[]{"i","am","look","for",
    //         "public","access","sourc","of","data","depict","braiand",
    //         "neuron","function"};

    //     for(int i = 0; i < expected.Length;i++){
    //         Assert.Equal(expected[i],result[i]);
    //     }
    // }
/*
    [Fact]
    public void TestPuttingWordsInHashMap(){
        FileReader fileReader = new FileReader(_foldersPath);
        Dictionary <string,string> result = fileReader.GetContentsOfFiles();
       // List<int> list = new List<int>{58913,58569,58578,58886,
       //     58912,58940,58965,59007,59105,59144,59183};
        
       // HashSet<int> expected = new HashSet<int>(list);
        bool x = result.TryGetValue("remind",out HashSet<int> y);

        Assert.Equal(,result["57110"]);
    }*/

}