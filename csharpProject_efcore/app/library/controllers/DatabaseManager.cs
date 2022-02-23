
using library.models;
using library.interfaces;

namespace library.controllers
{
    public class DatabaseManager : IDatabaseManager
    {
        public void InitializeDatabase(List<Student> students, List<Grade> grades)
        {
            var context = new SchoolContext();
            context.AddRange(students);
            context.SaveChanges();

            context = new SchoolContext();
            context.AddRange(grades);
            context.SaveChanges();
        }


        
        public List<(string, float)> QueryOnDatabase()
        {
            var context = new SchoolContext();

            return context.Students.Join(
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

        }
    }
}
