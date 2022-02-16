namespace library;
using library.interfaces;
using System.IO;

public class FileReader : IReader
{
    public string path {set; get;}
    public string Read() => File.ReadAllText(path);
}
