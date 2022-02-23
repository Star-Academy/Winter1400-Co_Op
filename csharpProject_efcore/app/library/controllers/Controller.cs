namespace library;
using library.models;
using library.interfaces;
using library.controllers;

public class Controller : IController
{
    public void Run(string studentsPath, string scoresPath, IOutput output){

        var students = DeserializeJsonArrayFileByPath<Student>(studentsPath);
        
        var grades = DeserializeJsonArrayFileByPath<Grade>(scoresPath);

        var databaseManager = new DatabaseManager();

        databaseManager.InitializeDatabase(students, grades);

        output.OutputList(GetSomeStudentsDataAsStrings(databaseManager.QueryOnDatabase(), 3));
    }

    private List<string> GetSomeStudentsDataAsStrings
        (List<(string name, float score)> scores, int numberOfStudentsToGet)
    {
        return scores
            .Take(numberOfStudentsToGet)
            .Select(x => $"{x.name} {x.score}")
            .ToList();
    }

    private List<T> DeserializeJsonArrayFileByPath <T>( string path){
        
        var scoresFileReader = new FileReader() {path = path};
        var scoresJsonData = scoresFileReader.Read();
        
        return new JsonDeserializer<T>().DeserializeArray(scoresJsonData);
    }



}

