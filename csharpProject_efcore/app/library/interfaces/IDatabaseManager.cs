
using library.models;

namespace library.interfaces
{
    public interface IDatabaseManager
    {
        public void InitializeDatabase(List<Student> students, List<Grade> grades);
        public List<string> Query();
    }
}
