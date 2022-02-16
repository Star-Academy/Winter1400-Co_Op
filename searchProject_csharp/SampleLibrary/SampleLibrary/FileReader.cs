namespace SampleLibrary
{
    public class FileReader
    {

        private string path;
        Dictionary<string,HashSet<int>> indexes = new Dictionary<string, HashSet<int>>();
        public FileReader(string path){
            this.path = path;
        }


        public FileInfo[] getAddressOfFiles(){
            DirectoryInfo di = new DirectoryInfo(path);
            FileInfo[] fileInfos = di.GetFiles();
            return fileInfos;
            


        }
        private void readFiles(FileInfo[] fileInfos){
            foreach(FileInfo fileInfo in fileInfos){
                readDataFromFile(fileInfo.FullName,fileInfo.Name);

            }
        }
        public Boolean readDataFromFile(string path,string name){
            string data = readContents(path);
            if(!isFileFound(data))
              return false;
            string[] words = processDataAndGiveWords(data);
            storeWords(words,name);
            return true;

        }
        private void storeWords(string[] words,string fileName){
            for(int i = 0; i < words.Length; i++){
                HashSet<int> fileIds = computeIfAbsent(words[i]);
                fileIds.Add(Convert.ToInt32(fileName));      
            }


        }
        private HashSet<int> computeIfAbsent(string word){
            HashSet<int> fileIds;
            bool exist = indexes.TryGetValue(word,out fileIds);
            if(exist)
            return fileIds;
            fileIds = new HashSet<int>();
            indexes.Add(word,fileIds);
            return fileIds;
        }
        private string readContents(string path){
            string data = File.ReadAllText(path);
            return data;

        }
        private Boolean isFileFound(string data){
            return data != null;
        }
        
        public string[] processDataAndGiveWords(string data){
            DocumentProcessor documentProcessor = new DocumentProcessor(data);
            return documentProcessor.getNormalizedWords();

        }
    }
}