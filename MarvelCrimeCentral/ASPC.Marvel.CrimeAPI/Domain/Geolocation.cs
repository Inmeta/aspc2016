using System;

namespace ASPC.Marvel.CrimeAPI
{
    [Serializable]
    public class Geolocation
    {
        public Geolocation()
        {
        }

        public Geolocation(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}