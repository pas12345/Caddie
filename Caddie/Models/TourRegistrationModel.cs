using System;

namespace Caddie.Models
{
    public class TourRegistrationModel
    {
        public int TourId { get; set; }
        public int VgcNo { get; set; }
        public bool IsRegistered { get; set; }
        public bool Open { get; set; }
        public int NoOfRegistrations { get; set; }
    }
}
