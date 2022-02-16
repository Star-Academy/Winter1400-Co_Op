namespace library;
using library.models;
using library.interfaces;
public class Controller : IController
{
        public void Run(string studentsPath, string scoresPath, IOutput output){

        var deserializedInformationOfStudents = 
            DeserializeJsonArrayFileByPath<StudentInformation>(studentsPath);
        
        var deserizlizedInformationOfScores = 
            DeserializeJsonArrayFileByPath<ScoreInformation>(scoresPath);
        
        var informationOfStudents = 
            ConvertListToDictionary<int, StudentInformation>
            (deserializedInformationOfStudents, s => s.studentNumber);

        AddScoresToInformationOfStudents
            (informationOfStudents, deserizlizedInformationOfScores);
        
        var listToOutput = GetInformationOfFirstThreeStudents
            (informationOfStudents.Values.ToList());
        
        output.OutputList(listToOutput);
    }
    private List<T> DeserializeJsonArrayFileByPath <T>(string path){
        var scoresFileReader = new FileReader() {path = path};
        var scoresJsonData = scoresFileReader.Read();
        return new JsonDeserializer<T>().DeserializeArray(scoresJsonData);
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

        return list.Select(element => element).
            ToDictionary(element => GetKey(element),element => element);
    }

    private List<string> GetInformationOfFirstThreeStudents
        (List <StudentInformation> information){
        
        return information.OrderByDescending
        (s => s.average).Take(3).Select(information => 
        $"{information.firstName} {information.lastName}" +
        $" : {information.average}").ToList();
    }

}

