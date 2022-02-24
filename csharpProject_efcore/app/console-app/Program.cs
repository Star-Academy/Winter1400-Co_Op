using console_app;
using library;
using library.controllers;

new Controller(Resource1.StudentsPath, Resource1.ScoresPath, new ConsoleOutput(), 
    new DatabaseInitializer(), new DatabaseQueryForFindingANumberOfBestAverages(3)).Run();
