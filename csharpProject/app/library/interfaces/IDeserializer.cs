namespace library.interfaces;

public interface IDeserializer<T>
{
     List<T> DeserializeArray (string serialized);
}
