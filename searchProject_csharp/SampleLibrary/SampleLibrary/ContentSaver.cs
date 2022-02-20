namespace SampleLibrary
{
    public class ContentSaver
    {
        private readonly Dictionary<string,HashSet<int>> indexes = 
            new Dictionary<string, HashSet<int>>();
        
        public Dictionary<string,HashSet<int>> Indexes
        {
            get
            {
                return indexes;
            }
        }

        public void StoreWords(string [] words,string fileName)
        {
            for(int i = 0; i < words.Length; i++)
            {
                HashSet<int> fileIds = ComputeIfAbsent(words[i]);
                fileIds.Add(Convert.ToInt32(fileName));
            }
        }
        
        private HashSet<int> ComputeIfAbsent(string word){
            HashSet<int> fileIds;
            bool exist = indexes.TryGetValue(word,out fileIds);
            if(exist)
                return fileIds;

            fileIds = new HashSet<int>();
            indexes.Add(word,fileIds);
            return fileIds;
        }

    }
}