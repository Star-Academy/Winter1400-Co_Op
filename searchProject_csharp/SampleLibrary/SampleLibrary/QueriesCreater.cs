using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleLibrary
{
    public class QueriesCreater
    {

        public Queries CreateQueries(string query)
        {
            Queries queries = new Queries();
            foreach (var word in query.Split(" "))
            {
                AddWordToQuery(queries, word);
            }
            return queries;
        }

        private void AddWordToQuery(Queries queries ,string word)
        {

            if (word.StartsWith("+"))
            {
                queries.plusQueries.Add(word.Substring(1));
            }
            else if (word.StartsWith("-"))
            {
                queries.minusQueries.Add(word.Substring(1));
            }
            else
            {
                queries.zeroQueries.Add(word);
            }
        }
    }
}
