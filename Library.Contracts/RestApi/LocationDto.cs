using System;
using Library.Contracts.Common;

namespace Library.Contracts.RestApi
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public LocationType Type { get; set; }
        public string Value { get; set; }
    }
}