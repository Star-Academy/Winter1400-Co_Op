using library.models;

namespace library.interfaces
{
    public interface IDatabaseInitializer
    {
        void InitializeDatabase(List<Student> students, List<Grade> grades);
    }
}
