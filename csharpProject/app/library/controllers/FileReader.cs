namespace library;
using library.interfaces;

public class FileReader : IReader
{
    public string path {set; get;}
    public string Read() => System.IO.File.ReadAllText(path);
}
