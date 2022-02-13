namespace library;
using Newtonsoft.Json;
public class FileReader
{
    public string path {set; get;}
    public string ReadFile() => System.IO.File.ReadAllText(path);
}

public class Student
{
    public string firstName{set; get;}
    public string lastName{set; get;}
    public int studentNumber{set; get;}
}

public class Score
{
    public float score {set; get;}
    public int studentNumber {set; get;}
    public string lesson{set; get;}
}

public static class Deserializer<T>
{
    public static List<T> DeserializeArrayOfJsonObjects(string jsonString) => 
        JsonConvert.DeserializeObject<List<T>>(jsonString);
}

public class Data{
    public int studentNumber{set; get;}
    public int count{set; get;} = 0;
    public string firstName{set; get;}
    public string lastName{set; get;}
    public float average{set; get;}
    public void AddScore(float newScore){
        average = ((average * count) + newScore) / ++count;
    }
}

public class controller{

    private List<Student> students;
    private List<Score> scores;
    private Dictionary<int, Data> allData;

    public void Run(){
        InitializeScores();
        InitializeStudendts();
        InitializeData();
        CalcuolateAverages();
        PrintAnswer(getFirstThreeStudents());
    }
    private void InitializeStudendts(){
        var studentsFileReader = new FileReader() {path = @"../files/students.txt"};
        var studentsJsonData = studentsFileReader.ReadFile();
        students = Deserializer<Student>.DeserializeArrayOfJsonObjects
            (studentsJsonData);
    }

    private void InitializeScores(){
        var scoresFileReader = new FileReader() {path = @"../files/scores.txt"};
        var scoresJsonData = scoresFileReader.ReadFile();
        scores = Deserializer<Score>.DeserializeArrayOfJsonObjects(scoresJsonData);
    }

    private void InitializeData(){
        allData = new Dictionary<int, Data>();

        foreach(var student in students){
            var eachData = new Data(){
                firstName = student.firstName,
                lastName = student.lastName,
                studentNumber = student.studentNumber
            };
            allData.Add(student.studentNumber, eachData);
        }
    }

    private void CalcuolateAverages(){
        foreach (var score in scores){
            var eachData = allData[score.studentNumber];
            eachData.AddScore(score.score);
        }
    }

    private List<Data> getFirstThreeStudents(){

        var data = allData.Values.ToList();
        data.Sort(new GFG());
        return data.GetRange(0,3);
    }

    private void PrintAnswer(List<Data>data){
        foreach (var eachData in data){
            Console.WriteLine(eachData.firstName + " " +
                eachData.lastName + " : " + eachData.average);
        }
    }
}

class GFG : IComparer<Data>{
    public int Compare(Data x, Data y)
    {
        return x.average - y.average >= 0 ? -1 : 1;
    }
}