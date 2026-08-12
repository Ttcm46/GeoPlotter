using GeoPlotter.Clases;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeoPlotter.Automation
{
    internal class Geography
    {
        /// <summary>
        /// Gets the current location of the device using geolocation services and updates the provided JsonData object with latitude, longitude, altitude, and type information.
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static async Task<JsonData> GetCurrentLocation(JsonData jsonData)
        {
            try
            {
                CancellationTokenSource _cancelTokenSource;
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                _cancelTokenSource = new CancellationTokenSource();

                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

                if (location != null)
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    jsonData.Latitud = location.Latitude;
                    jsonData.Longitud = location.Longitude;
                    jsonData.Altitude = location.Altitude;
                    jsonData.Type = "Geography";
            }
            // Catch one of the following exceptions:
            //   FeatureNotSupportedException
            //   FeatureNotEnabledException
            //   PermissionException
            catch (Exception ex)
            {
                Console.WriteLine   (ex.ToString());    
            }
            return jsonData;
        }
    }
}
