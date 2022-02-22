using library;
using library.controllers;

new Controller().Run(studentsPath : "../../../../files/students.txt", 
    scoresPath : "../../../../files/scores.txt", new ConsoleOutput());
