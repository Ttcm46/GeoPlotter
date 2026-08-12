using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPlotter.Automation
{
    internal class JsonManager
    {
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static string SerializeJson<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
        public static string saveJson<T>(T obj, string fileName)
        {
            string cacheDir = FileSystem.Current.CacheDirectory;
            string dataDir = FileSystem.Current.AppDataDirectory;

            string json = SerializeJson(obj);
            FileStream fileStream = new FileStream(System.IO.Path.Combine(dataDir, fileName), FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileStream);
            writer.WriteLine(json);
            writer.Close();

            return dataDir;

        }
        public static string loadJson<T>(out T obj, string fileName )
        {
            string dataDir = FileSystem.Current.AppDataDirectory;
            string json = File.ReadAllText(System.IO.Path.Combine(dataDir, fileName));
            obj = DeserializeJson<T>(json);
            return obj.ToString();  

        }
    }
}
