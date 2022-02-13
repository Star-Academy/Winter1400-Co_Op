namespace library;

public class Controller
{
    private List<StudentInformation> deserializedStudentsInformation;
    private List<ScoreInformation> deserizlizedScoresInformation;
    private Dictionary<int, StudentInformation> studentsInformation;

    private void InitializeDeserializedStudentsInformation(){
        var studentsFileReader = new FileReader() {path = @"../files/students.txt"};
        var studentsJsonData = studentsFileReader.ReadFile();
        deserializedStudentsInformation = Deserializer<StudentInformation>
            .DeserializeArrayOfJsonObjects(studentsJsonData);
    }

    private void InitializeDeserizlizedScoresInformation(){
        var scoresFileReader = new FileReader() {path = @"../files/scores.txt"};
        var scoresJsonData = scoresFileReader.ReadFile();
        deserizlizedScoresInformation = Deserializer<ScoreInformation>.DeserializeArrayOfJsonObjects(scoresJsonData);
    }

    private void InitializeStudentsInformation(){
        studentsInformation = new Dictionary<int, StudentInformation>();

        foreach(var student in deserializedStudentsInformation){
            var eachData = new StudentInformation(){
                firstName = student.firstName,
                lastName = student.lastName,
                studentNumber = student.studentNumber
            };
            studentsInformation.Add(student.studentNumber, eachData);
        }
    }

    private void CalculateAverages(){
        foreach (var score in deserizlizedScoresInformation){
            var eachData = studentsInformation[score.studentNumber];
            eachData.AddScore(score.score);
        }
    }

    private List<StudentInformation> getFirstThreeStudents(){

        var data = studentsInformation.Values.ToList();
        data.Sort(new StudentsComparator());
        return data.GetRange(0,3);
    }

    private void PrintAnswer(List<StudentInformation>data){
        foreach (var eachData in data){
            Console.WriteLine(eachData.firstName + " " +
                eachData.lastName + " : " + eachData.average);
        }
    }
    
    public void Run(){
        InitializeDeserizlizedScoresInformation();
        InitializeDeserializedStudentsInformation();
        InitializeStudentsInformation();
        CalculateAverages();
        PrintAnswer(getFirstThreeStudents());
    }
}

