using System;

namespace Business
{
    public class MediaRequest
    {
        public int MediaID { get; set; }
        public string RequestType { get; set; }
        public string VIN { get; set; }
        public string Message { get; set; }
        public string OBDID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string GPSAccuracy { get; set; }
        public DateTime GPSSeenTime { get; set; }
        public string RSSI { get; set; }
        public DateTime TimeLastSeen { get; set; }
        public DateTime TimeOfRequest { get; set; }
        public int RequestorID { get; set; }
        public int SenderID { get; set; }
        public string Media { get; set; }
    }
}
