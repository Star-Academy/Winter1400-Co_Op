namespace SampleLibrary
{
    public class FileReader : IFileReader
    {
        private readonly string path;
    
        public FileReader(string path)
        {
            this.path = path;
        }
        public Dictionary<string,string> GetContentsOfFiles()
        {
            FileInfo[] files = getAddressOfFiles();
            return readFiles(files);      
        }
        public FileInfo[] getAddressOfFiles()
        {
            var directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles();       
        }
        private Dictionary<string,string> readFiles(FileInfo[] fileInfos)
        {
            return fileInfos.Where(file=> file != null).
                ToDictionary(file=> file.Name,file => ReadDataFromFile
                (file.FullName));
        }
        public string ReadDataFromFile(string path)
        {
            try{
                return File.ReadAllText(path);
            }
            catch(FileNotFoundException){
                return null;
            }
        }      
    }
}