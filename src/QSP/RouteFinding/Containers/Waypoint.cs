using QSP.RouteFinding.Data.Interfaces;
using System;

namespace QSP.RouteFinding.Containers
{
    public class Waypoint : IComparable<Waypoint>, ICoordinate, IEquatable<Waypoint>
    {
        public const int DefaultCountryCode = -1;

        public string ID { get; }
        public double Lat { get; }
        public double Lon { get; }
        public int CountryCode { get; }

        public Waypoint(
            string ID,
            double Lat = 0.0,
            double Lon = 0.0,
            int CountryCode = DefaultCountryCode)
        {
            this.ID = ID;
            this.Lat = Lat;
            this.Lon = Lon;
            this.CountryCode = CountryCode;
        }

        public Waypoint(string ID, ICoordinate latLon) : this(ID, latLon.Lat, latLon.Lon) { }

        public Waypoint(Waypoint w) : this(w.ID, w.Lat, w.Lon, w.CountryCode) { }

        /// <summary>
        /// Determines whether ID, Lat, and Lon match.
        /// </summary>
        public bool Equals(Waypoint x)
        {
            return x != null &&
                ID == x.ID &&
                Lat == x.Lat &&
                Lon == x.Lon;
        }

        public int CompareTo(Waypoint other)
        {
            int x = ID.CompareTo(other.ID);

            if (x == 0)
            {
                int y = Lat.CompareTo(other.Lat);
                if (y == 0) return Lon.CompareTo(other.Lon);
                return y;
            }

            return x;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode() ^ Lat.GetHashCode() ^ Lon.GetHashCode();
        }
    }
}
