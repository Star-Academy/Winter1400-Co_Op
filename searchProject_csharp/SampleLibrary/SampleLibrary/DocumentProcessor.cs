using System.Text.RegularExpressions;
{
    
}
namespace SampleLibrary
{
    public class DocumentProcessor
    {
        private string data;
        private static readonly Stemmer stemmer = new Stemmer();
        public DocumentProcessor(string data){
            this.data = data;
        }
        public string[] getNormalizedWords(){
            toLowerCase();
            removeSigns();
            return toStemSplit();

        }
        private void toLowerCase(){
            if(data == null)
            return;
            else
            data = data.ToLower();

        }
        private void removeSigns(){
            if(data == null)
            return;
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
            if(data == null)
            return new string[0];
            string[] splittedData = splitData();
            return stemSplitData(splittedData);
        }
        private string[] stemSplitData(string[] splittedData){
            for(int i = 0; i < splittedData.Length; i++){
                splittedData[i] = stemmer.stemWord(splittedData[i]);
            }
            return splittedData;
        }
        private string[] splitData(){
            return data.Split("\\s+");
        }

    }

}