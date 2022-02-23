
using library.models;

namespace library.interfaces
{
    public interface IDatabaseManager
    {
        void InitializeDatabase(List<Student> students, List<Grade> grades);
        List<(string, float)> QueryOnDatabase();
    }
}
