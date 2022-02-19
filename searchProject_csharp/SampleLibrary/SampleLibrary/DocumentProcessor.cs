using System.Text.RegularExpressions;
namespace SampleLibrary
{
    public class DocumentProcessor
    {
        private  string data;
        private static readonly Stemmer stemmer = new Stemmer();
        public DocumentProcessor(string data){
             if(data == null)
             throw new Exception("data is null");
             this.data = data.ToLower();        
        }
        public string[] getNormalizedWords(){
            removeSigns();
            return toStemSplit();
        }
       
        private void removeSigns(){
           removeDashes();
           removeNonNumericalSigns();
        }
        private void removeDashes(){
            Regex regex = new Regex("[-?]+");
            data = regex.Replace(data," ");
        }
        private void removeNonNumericalSigns(){
            Regex regex = new Regex("[^a-zA-Z0-9\\s]+");
            data = regex.Replace(data," ");
        }
        private string[] toStemSplit(){
            var splittedData = data.Split(" ");
            return stemSplitData(splittedData);
        }
        private string[] stemSplitData(string[] splittedData){
            for(int i = 0; i < splittedData.Length; i++){
                splittedData[i] = stemmer.StemWord(splittedData[i]);
            }
            return splittedData;
        }
    }
}