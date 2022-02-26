
using library.models;
using library.interfaces;

namespace library.controllers
{
    public class DatabaseInitializer : IDatabaseInitializer
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
       
    }
}
