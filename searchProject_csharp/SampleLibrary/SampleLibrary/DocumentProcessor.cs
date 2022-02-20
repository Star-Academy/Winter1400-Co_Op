using System.Text.RegularExpressions;
namespace SampleLibrary
{
    public class DocumentProcessor
    {
        private readonly string data;
        private static readonly Stemmer stemmer = new Stemmer();
        public DocumentProcessor(string data)
        {
            if(data == null)
                throw new Exception("data is null");
            this.data = data.ToLower();
            
                   
        }
        public string[] getNormalizedWords()
        {
            return toStemSplit(removeSigns());
        }

        private string removeSigns()
        {
            return removeNonNumericalSigns(removeDashes());
        }

        private string removeDashes()
        {
            var regex = new Regex("[-?]+");
            return regex.Replace(data," ");
        }

        private string removeNonNumericalSigns(string temp)
        {
            var regex = new Regex("[^a-zA-Z0-9\\s]+");
            return regex.Replace(temp," ");
        }
        
        private string[] toStemSplit(string temp)
        {
            var splittedData = temp.Split(" ");
            return stemSplitData(splittedData);
        }

        private string[] stemSplitData(string[] splittedData)
        {
            for(int i = 0; i < splittedData.Length; i++)
            {
                splittedData[i] = stemmer.StemWord(splittedData[i]);
            }
            return splittedData;
        }

    }
}