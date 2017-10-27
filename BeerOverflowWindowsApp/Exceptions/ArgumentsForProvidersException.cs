using System;
using System.Collections.Generic;

namespace BeerOverflowWindowsApp.Exceptions
{
    [Serializable()]
    public class ArgumentsForProvidersException : Exception
    {
        public string InvalidArguments { get; }

        public ArgumentsForProvidersException() : base() { }
        public ArgumentsForProvidersException(string message) : base(message) { }
        public ArgumentsForProvidersException(string message, Exception inner) : base(message, inner) { }

        public ArgumentsForProvidersException(string message, string latitude, string longitude, string radius) :
            base(message)
        {
            var latitudeIsCorrect = RegexTools.LatitudeTextIsCorrect(latitude);
            var longitudeIsCorrect = RegexTools.LongitudeTextIsCorrect(longitude);
            var radiusIsCorrect = RegexTools.RadiusTextIsCorrect(radius);

            var badArguments = new List<string>();

            if (!latitudeIsCorrect) { badArguments.Add("latitude"); }
            if (!longitudeIsCorrect) { badArguments.Add("longitude"); }
            if (!radiusIsCorrect) { badArguments.Add("radius"); }
            InvalidArguments = string.Join(",", badArguments);
        }

        protected ArgumentsForProvidersException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
