namespace SampleLibrary
{
    public class Queries
    {
        public List<string> zeroQueries {set; get;} = new ();
        public List<string> plusQueries {set; get;} = new ();
        public List<string> minusQueries {set; get;} = new ();
        public Queries(string query){
            foreach(var word in query.Split(" "))
            {
                AddWordToQuery(word);
            }
        }


        private void AddWordToQuery(string word){

            if (word.StartsWith("+"))
            {
                plusQueries.Add(word.Substring(1));
            } 
            else if (word.StartsWith("-"))
            {
                minusQueries.Add(word.Substring(1));
            }
            else
            {
                zeroQueries.Add(word);
            }
        }
    }


}