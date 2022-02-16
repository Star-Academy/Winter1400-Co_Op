namespace library;

public class StudentInformation
{
    public int StudentNumber{set; get;}
    public string FirstName{set; get;}
    public string LastName{set; get;}
    public int Count{set; get;} = 0;
    public float Average{set; get;} = 0;
    public void AddScore(float newScore){
        Average = ((Average * Count) + newScore) / ++Count;
    }
}
