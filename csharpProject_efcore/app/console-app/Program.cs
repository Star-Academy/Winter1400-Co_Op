using console_app;
using library;
using library.controllers;

var controller = new Controller(Resource1.StudentsPath,
    Resource1.ScoresPath,
    new ConsoleOutput(),
    new DatabaseInitializer(),
    new DatabaseQueryForFindingANumberOfBestAverages(3));

controller.Run();
