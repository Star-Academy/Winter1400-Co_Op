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
        string falsePath = $"{foldersPath}/57111";
        Assert.Null(fileReader.ReadDataFromFile(falsePath));
    }

}


