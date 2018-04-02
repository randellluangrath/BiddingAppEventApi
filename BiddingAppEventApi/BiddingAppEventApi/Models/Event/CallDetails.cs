using System;

namespace WebAppEventApi.Models.Event
{
    public class CallDetails
    {
        public string originatingNumber { get; set; }

        public string receivingNumber { get; set; }

        public DateTime? callDate { get; set; }

        public DateTime? startTime { get; set; }

        public DateTime? callDuration { get; set; }

        public string callType { get; set; }

        public string isConnected { get; set; }

    }
}