using Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;

namespace Tools
{
    public static class JsonConverterTool
    {
        public static string SerializeData<T>(T data)
        {
            JsonSerializerSettings options = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            string jsonString = JsonConvert.SerializeObject(data, options);
            return jsonString;
        }

        public static T DeserializeObject<T>(string jsonString)
        {

            JsonSerializerSettings options = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

            T deserializeObject = JsonConvert.DeserializeObject<T>(jsonString, options);

            return deserializeObject;
        }
    }
}