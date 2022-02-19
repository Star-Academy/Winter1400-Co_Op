namespace SampleLibrary
{
    public class FileReader : IFileReader
    {

        private readonly string _path;
        

        public FileReader(string path){
            _path = path;
        }


        public FileInfo[] getAddressOfFiles(){
            var directoryInfo = new DirectoryInfo(_path);
            return directoryInfo.GetFiles();       
        }
        private Dictionary<string,string> readFiles(FileInfo[] fileInfos){
            return fileInfos.Where(file=> file != null).ToDictionary(file=> file.Name,file => readDataFromFile(file.FullName,file.Name));
        }
        public string readDataFromFile(string path,string name){
            try{
                return File.ReadAllText(path);
            }
            catch(FileNotFoundException){
                return null;
            }
        }      
           
        public Dictionary<string,string> GetContentsOfFiles(){
            FileInfo[] files = getAddressOfFiles();
            return readFiles(files);      
        }
    }
}