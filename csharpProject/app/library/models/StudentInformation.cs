namespace library;

public class StudentInformation
{
    public int studentNumber{set; get;}
    public string firstName{set; get;}
    public string lastName{set; get;}
    public int count{set; get;} = 0;
    public float average{set; get;} = 0;
    public void AddScore(float newScore){
        average = ((average * count) + newScore) / ++count;
    }
}
