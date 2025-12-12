using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.ValueObjects
{
    public class Coordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        private Coordinates() { }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
