using System;

namespace ASPC.Marvel.CrimeAPI
{
    public class Crime : Node
    {
        public Geolocation Location { get; set; }
        public string Source { get; set; }
    }
}