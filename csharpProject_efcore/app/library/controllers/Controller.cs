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

        output.OutputList(databaseManager.Query());
    }

    
    private List<T> DeserializeJsonArrayFileByPath <T>( string path){
        
        var scoresFileReader = new FileReader() {path = path};
        var scoresJsonData = scoresFileReader.Read();
        
        return new JsonDeserializer<T>().DeserializeArray(scoresJsonData);
    }



}

