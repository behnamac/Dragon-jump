using Tools;
using UnityEngine;

namespace Storage
{
    public class StorageAdaptor
    {
        public void SaveData<T>(string key, T data)
        {
            string jsonString = JsonConverterTool.SerializeData(data);
            // Debug.Log($"SET DATA : {jsonString}");
            PlayerPrefs.SetString(key, jsonString);
        }

        public T GetData<T>(string key)
        {
            string jsonString = PlayerPrefs.GetString(key);
            // Debug.Log($"GET DATA: {jsonString}");
            T data = JsonConverterTool.DeserializeObject<T>(jsonString);
            return data;
        }
    }
}