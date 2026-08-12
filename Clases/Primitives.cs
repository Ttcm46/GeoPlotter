using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GeoPlotter.Clases
{
    public class JsonData
    {
        public double? Latitud { get; set; }
        public double? Longitud { get; set; }
        public double? Altitude { get; set; }
        public string Type { get; set; }
        public string label { get; set; }
            
        public string toString()
        {
            return $"Latitud: {Latitud}, Longitud: {Longitud}, Altitude: {Altitude} ";
        }
    }
    public class JsonDataList
    {
        public List<JsonData> DataList { get; set; } = new List<JsonData>();
        public void Add(JsonData data)
        {
            DataList.Add(data);
        }
        public void Remove(JsonData data)
        {
            DataList.Remove(data);
        }
        public void Clear()
        {
            DataList.Clear();
        }
        public string toString()
        {
            string result = "";
            foreach (var data in DataList)
            {
                result += data.toString() + "\n";
            }
            return result;
        }
        public string exportJson()
        {
            // Serialize the internal list to a formatted JSON string
            return JsonConvert.SerializeObject(DataList, Formatting.Indented);
        }
    }

}
