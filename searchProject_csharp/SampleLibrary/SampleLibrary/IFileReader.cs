namespace SampleLibrary
{
    public interface IFileReader
    {
         public Dictionary<string, HashSet<int>> GetIndexes();
    }
}