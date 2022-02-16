namespace library.controllers;

using library.interfaces;

public class ConsoleOutput : IOutput
{
    public void OutputList<T>(List<T> list){
        list.ForEach(x => Console.WriteLine(x));
    }
}
