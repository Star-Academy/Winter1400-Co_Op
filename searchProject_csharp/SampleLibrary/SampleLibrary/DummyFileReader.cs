using System.Collections.Generic;

namespace SampleLibrary
{
    public class DummyFileReader : IFileReader
    {
        public Dictionary<string, HashSet<int>> tempDictionary;
        public Dictionary<string, HashSet<int>> GetIndexes(){
            return tempDictionary;
        }
    }
}