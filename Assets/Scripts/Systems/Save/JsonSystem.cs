using System.IO;
using UnityEngine;

namespace Assets.Scripts.Systems.Save
{
    public class JsonSystem
    {
        public static void SaveJson<T>(T obj, string name)
        {
            string savePath = Path.Combine(Application.persistentDataPath, name);

            string jsonData = JsonUtility.ToJson(obj, true);
            
            File.WriteAllText(savePath, jsonData);
        }

        public static bool GetJson<T>(string name, out T obj)
        {
            string getPath = Path.Combine(Application.persistentDataPath, name);

            if (!File.Exists(getPath))
            {
                obj = default;
                return false;
            }

            string jsonData = File.ReadAllText(getPath);
            obj = JsonUtility.FromJson<T>(jsonData);

            return true;
        }
    }

}
