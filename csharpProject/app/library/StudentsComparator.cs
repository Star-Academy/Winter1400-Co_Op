namespace library;

class StudentsComparator : IComparer<StudentInformation>{
    public int Compare(StudentInformation x, StudentInformation y)
    {
        return x.average - y.average >= 0 ? -1 : 1;
    }
}