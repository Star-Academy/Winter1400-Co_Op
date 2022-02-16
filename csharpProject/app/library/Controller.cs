namespace library;

public class Controller
{
    private List<T> DeserializeJsonArrayFileByPath <T>(string path){
        var scoresFileReader = new FileReader() {path = path};
        var scoresJsonData = scoresFileReader.ReadFile();
        return Deserializer<T>
         .DeserializeArrayOfJsonObjects(scoresJsonData);
    }

    private void AddScoresToInformationOfStudents(
        Dictionary<int, StudentInformation> studentInformation, 
        List <ScoreInformation> scoreInformation)
    {
        foreach (var score in scoreInformation)
        {
            studentInformation[score.studentNumber].AddScore(score.score);
        }
    }

    private Dictionary<Key, Value> ConvertListToDictionary <Key, Value>
     (List<Value> list, Func<Value, Key> GetKey){

        return list.
            Select(element => element).
            ToDictionary(element => GetKey(element),element => element);
    }

    public void Run(string studentsPath, string scoresPath){
        var deserializedInformationOfStudents = 
            DeserializeJsonArrayFileByPath<StudentInformation>(studentsPath);
        
        var deserizlizedInformationOfScores = 
            DeserializeJsonArrayFileByPath<ScoreInformation>(scoresPath);
        
        var informationOfStudents = 
            ConvertListToDictionary<int, StudentInformation>
            (deserializedInformationOfStudents, s => s.studentNumber);

        AddScoresToInformationOfStudents
            (informationOfStudents, deserizlizedInformationOfScores);
        
        informationOfStudents.Values.ToList().OrderByDescending(s => s.average)
            .Take(3).ToList().ForEach(
            information => Console.WriteLine(
            $"{information.firstName} {information.lastName}" +
            $" : {information.average}"));
    }
}

