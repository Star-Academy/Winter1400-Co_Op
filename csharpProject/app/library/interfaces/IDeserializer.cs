namespace library.interfaces;

public interface IDeserializer<T>
{
    public List<T> DeserializeArray (string serialized);
}
