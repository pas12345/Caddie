using System;

namespace Caddie.Models
{
    public class TourModel
    {
        public int Id { get; set; }
        public DateTime TourDate { get; set; }
        public string Description { get; set; }
        public DateTime LastRegistrationDate { get; set; }
        public bool OpenForSignUp { get; set; }
        public int MaxNoOfMembers { get; set; }
        public int NoOfMembers { get; set; }
        public string UrlDescription { get; set; }
        public int SponsorLogoId { get; set; }
    }
}
