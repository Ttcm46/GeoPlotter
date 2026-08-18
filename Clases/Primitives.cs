using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GeoPlotter.Clases
{
    // Geometry object for GeoJSON Feature (RFC 7946)
    public class Geometry
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "Point";

        [JsonProperty("coordinates")]
        public double[] Coordinates { get; set; } = new double[0];
    }

    // Properties object inside a GeoJSON Feature
    public class FeatureProperties
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("altitude")]
        public double? Altitude { get; set; }
    }

    // JsonData represents a GeoJSON Feature per RFC 7946
    public class JsonData
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "Feature";

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; } = new Geometry();

        [JsonProperty("properties")]
        public FeatureProperties Properties { get; set; } = new FeatureProperties();

        // Backwards-compatible helpers
        [JsonIgnore]
        public double? Latitud
        {
            get => (Geometry?.Coordinates != null && Geometry.Coordinates.Length > 1) ? Geometry.Coordinates[1] : null;
            set
            {
                if (Geometry?.Coordinates == null || Geometry.Coordinates.Length < 2)
                    Geometry.Coordinates = new double[2];
                Geometry.Coordinates[1] = value ?? 0.0;
            }
        }

        [JsonIgnore]
        public double? Longitud
        {
            get => (Geometry?.Coordinates != null && Geometry.Coordinates.Length > 0) ? Geometry.Coordinates[0] : null;
            set
            {
                if (Geometry?.Coordinates == null || Geometry.Coordinates.Length < 2)
                    Geometry.Coordinates = new double[2];
                Geometry.Coordinates[0] = value ?? 0.0;
            }
        }

        [JsonIgnore]
        public double? Altitude
        {
            get => Properties?.Altitude;
            set => Properties.Altitude = value;
        }

        [JsonIgnore]
        public string label
        {
            get => Properties?.Label;
            set => Properties.Label = value;
        }

        public override string ToString()
        {
            var lon = Longitud?.ToString() ?? "?";
            var lat = Latitud?.ToString() ?? "?";
            var alt = Altitude?.ToString() ?? "?";
            var name = Properties?.Name ?? Properties?.Label ?? string.Empty;
            return $"Feature: {name} - Coordinates: [{lon}, {lat}], Altitude: {alt}";
        }

        // Returns a GeoJSON string for this feature
        public string ToGeoJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
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
            var sb = new StringBuilder();
            foreach (var data in DataList)
            {
                sb.AppendLine(data.ToString());
            }
            return sb.ToString();
        }

        // Serialize the list of Features to a GeoJSON FeatureCollection
        public string exportJson()
        {
            var featureCollection = new
            {
                type = "FeatureCollection",
                features = DataList
            };

            return JsonConvert.SerializeObject(featureCollection, Formatting.Indented);
        }
    }

}
