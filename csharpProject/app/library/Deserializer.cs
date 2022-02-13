using Newtonsoft.Json;

namespace library;
public static class Deserializer<T>
{
    public static List<T> DeserializeArrayOfJsonObjects(string jsonString) => 
        JsonConvert.DeserializeObject<List<T>>(jsonString);
}

