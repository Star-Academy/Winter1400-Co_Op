using Newtonsoft.Json;
using library.interfaces;

namespace library;
public class JsonDeserializer<T> : IDeserializer<T>
{
    public List<T> DeserializeArray(string serialized) => 
        JsonConvert.DeserializeObject<List<T>>(serialized);
}

