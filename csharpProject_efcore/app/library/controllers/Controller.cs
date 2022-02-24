namespace library;
using library.models;
using library.interfaces;
using library.controllers;

public class Controller : IController
{
    private readonly string _studentsPath;
    private readonly string _scoresPath;
    private readonly IOutput _output;
    private readonly IDatabaseInitializer _databaseInitializer;
    private readonly IDatabaseQuery _databaseQuery;

    public Controller(string studentsPath, string scoresPath, IOutput output,
        IDatabaseInitializer databaseInitializer, IDatabaseQuery databaseQuery)
    {
        _studentsPath = studentsPath;
        _scoresPath = scoresPath;
        _output = output;
        _databaseInitializer = databaseInitializer;
        _databaseQuery = databaseQuery;
    }

    public void Run(){

        initializeDatabase();

        _output.OutputList(_databaseQuery.QueryOnDatabase());
    }

    private void initializeDatabase()
    {
        var students = DeserializeJsonArrayFileByPath<Student>(_studentsPath);

        var grades = DeserializeJsonArrayFileByPath<Grade>(_scoresPath);

        _databaseInitializer.InitializeDatabase(students, grades);
    }

    private List<T> DeserializeJsonArrayFileByPath <T>( string path){
        
        var scoresFileReader = new FileReader() {path = path};
        var scoresJsonData = scoresFileReader.Read();
        
        return new JsonDeserializer<T>().DeserializeArray(scoresJsonData);
    }

}

