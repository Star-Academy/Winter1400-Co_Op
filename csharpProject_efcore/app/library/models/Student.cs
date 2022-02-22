using System.ComponentModel.DataAnnotations;

namespace library;

public class Student
{
    [Key]
    public int StudentNumber { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
}
