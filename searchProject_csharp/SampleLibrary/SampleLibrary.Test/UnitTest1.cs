<<<<<<< HEAD
using Xunit;
using SampleLibrary;
using System.IO;
//using FluentAssertions;
namespace SampleLibrary.Test;

public class UnitTest1
{
    [Fact]
    public void TestImportFiles()
    {
     //   int number = 1;
      //  number.Should().BeEqual(1);
     // Assert.Equal(1,2);
     FileReader fileReader = new FileReader("C:\\Users\\Alireza\\Desktop\\mohProject\\Winter1400-Co_Op\\files");
    string[] addresses =  fileReader.getAddressOfFiles();
    Assert.Equal(1000,addresses.Length);          
    }
    [Fact]
    public void TestReadingFile(){
        FileReader fileReader = new FileReader("files");
        FileInfo[] fileInfos = fileReader.getAddressOfFiles();
        Assert.True(fileReader.readDataFromFile(fileInfos[0].FullName,fileInfos[0].Name));

    }
    [Fact]
    public void TestReadingFile2(){
        FileReader fileReader = new FileReader("files");
        string falsePath = @"C:\Users\Alireza\Desktop\mohProject\Winter1400-Co_Op\searchProject_csharp\SampleLibrary\files\57111";
        string falseName = "57111";
        Assert.False(fileReader.readDataFromFile(falsePath,falseName));
    }

=======
using Xunit;

namespace SampleLibrary.Test;

public class UnitTest1
{

    
    [Fact]
    public void Test1()
    {


    }
>>>>>>> 5e18e803773eeab62cd9e799cce4c01466046472
}