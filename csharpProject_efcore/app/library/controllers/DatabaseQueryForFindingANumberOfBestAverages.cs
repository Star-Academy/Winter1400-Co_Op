using library.interfaces;
using library.models;

namespace library.controllers
{
    public class DatabaseQueryForFindingANumberOfBestAverages : IDatabaseQuery
    {

        private readonly int _numberOfStudentsToGet;

        public DatabaseQueryForFindingANumberOfBestAverages(int numberOfStudentsToGet)
        {
            _numberOfStudentsToGet = numberOfStudentsToGet;
        }

        public List<string> QueryOnDatabase()
        {
            var context = new SchoolContext();

            var extractedDataFromDatabase = context.Students.Join(
                context.Grades,
                student => student.StudentNumber,
                grade => grade.StudentNumber,
                (student, grade) => new
                {
                    StdID = grade.StudentNumber,
                    StdName = $"{student.FirstName} {student.LastName}",
                    Score = grade.Score
                }
            ).GroupBy(x => x.StdID).Select
            (x => new
            {
                name = x.First().StdName,
                score = x.Average(p => p.Score)
            }).OrderByDescending(x => x.score).ToList()
            .Select(x => (x.name, x.score)).ToList();

            return GetSomeStudentsDataAsStrings(extractedDataFromDatabase);
        }

        private List<string> GetSomeStudentsDataAsStrings
        (List<(string name, float score)> scores)
        {
            return scores
                .Take(_numberOfStudentsToGet)
                .Select(x => $"{x.name} {x.score}")
                .ToList();
        }
    }
}
