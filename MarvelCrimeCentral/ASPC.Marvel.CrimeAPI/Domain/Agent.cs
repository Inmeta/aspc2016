using System;

namespace ASPC.Marvel.CrimeAPI
{
    public class Agent : Node
    {
        public Geolocation Location { get; set; }
        public int BPM { get; set; }
        public int GSR { get; set; }
        public int UV { get; set; }
        public int Barometer { get; set; }
    }
}