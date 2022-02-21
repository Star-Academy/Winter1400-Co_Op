namespace SampleLibrary
{
    public class DataIsNullException : Exception
    {
        public DataIsNullException(string message) : base(message){}
    }
}