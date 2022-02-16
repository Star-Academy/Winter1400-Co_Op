using System.Collections.Generic;

namespace SampleLibrary
{
    public class DummyFileReader : IFileReader
    {
        public Dictionary<string, HashSet<int>> TempDictionary;
        public Dictionary<string, HashSet<int>> GetIndexes(){
            return TempDictionary;
        }
    }
}