using System.ComponentModel.DataAnnotations.Schema;

namespace library.models;

public class Grade
{
    public int Id { get; set; }
    public float Score { set; get; }
    public int StudentNumber { set; get; }
    public string Lesson { set; get; }

    [ForeignKey("StudentNumber")]
    public Student student { get; set; }
}
