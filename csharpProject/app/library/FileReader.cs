namespace library
{
    public class FileReader
    {
        public string path {set; get;}
        public string ReadFile() => System.IO.File.ReadAllText(path);
    }
}