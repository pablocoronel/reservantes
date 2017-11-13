namespace CapaServicio.Dto
{
    public class GoogleMapsDto
    {
        public object Address { get; internal set; }
        public object Latitude { get; internal set; }
        public object Longitude { get; internal set; }
        public object PostalCode { get; internal set; }
        public string Status { get; internal set; }
        public object StreetName { get; internal set; }
    }
}