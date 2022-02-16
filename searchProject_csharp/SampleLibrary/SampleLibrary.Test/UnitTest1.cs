using Xunit;
using SampleLibrary;
using System.IO;
//using FluentAssertions;
using System.Collections.Generic;
namespace SampleLibrary.Test;


public class UnitTest1
{
    [Fact]
    public void TestImportFiles()
    {
     //   int number = 1;
      //  number.Should().BeEqual(1);
     // Assert.Equal(1,2);
    FileReader fileReader = new FileReader(@"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files");
    FileInfo[] addresses =  fileReader.getAddressOfFiles();
    Assert.Equal(1000,addresses.Length);          
    }
    [Fact]
    public void TestReadingFile(){
        FileReader fileReader = new FileReader(@"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files");
        FileInfo[] fileInfos = fileReader.getAddressOfFiles();
        Assert.True(fileReader.readDataFromFile(fileInfos[0].FullName,fileInfos[0].Name));

    }
    [Fact]
    public void TestReadingFile2(){
        FileReader fileReader = new FileReader(@"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files");
        string falsePath = @"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files\57111";
        string falseName = "57111";
        Assert.False(fileReader.readDataFromFile(falsePath,falseName));
    }
    [Fact]
    public void TestProcessData(){
        string data = "I am looking for publically accessible sources of data depicting braiand neuron functions.";
        FileReader fileReader = new FileReader(@"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files");
        string[] result = fileReader.processDataAndGiveWords(data);
        string[] expected = new string[]{"i","am","look","for","public","access","sourc","of","data","depict","braiand","neuron","function"};
        for(int i = 0; i < expected.Length;i++){
            Assert.Equal(expected[i],result[i]);
        }

    }
    [Fact]
    public void TestPuttingWordsInHashMap(){
        FileReader fileReader = new FileReader(@"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\files");
        Dictionary <string,HashSet<int>> result = fileReader.GetIndexes();
        List<int> list = new List<int>{58913,58569,58578,58886,58912,58940,58965,59007,59105,59144,59183};
        HashSet<int> expected = new HashSet<int>(list);
        bool x = result.TryGetValue("remind",out HashSet<int> y);
        Assert.Equal(expected,y);

    }



}