using System;
using System.Globalization;

namespace Caddie.Models
{
    public class MatchModel 
    {
        public MatchModel() 
        {
            ClubName = string.Empty;
            CourseName = string.Empty;
            MatchDate = new DateTime(2000, 1, 1);
            MatchText = "";
        }

        public int Id { get; set; }
        public string ClubName { get; set; }
        public int CourseDetailId { get; set; }
        public string CourseName { get; set; }
        public string CourseText
        {
            get
            {
                return string.Format(CultureInfo.InstalledUICulture, "{0}, {1}", this.ClubName.Trim(), this.CourseName);
            }
        }
        public string Tee { get; set; }
        public int Par { get; set; }
        public DateTime MatchDate { get; set; }
        public string MatchDateString 
        {
            get { return MatchDate.ToString("dddd d MMM", new CultureInfo("DA")); }
        }
        public string MatchText { get; set; }
        public string MonthOfEvent { get; set; }
        public string Sponsor { get; set; }
        public int SponsorLogoId { get; set; }
        public string Matchform { get; set; }
        public int MatchformId { get; set; }
        public bool IsStrokeplay { get { return MatchformId == 1; } }
        public bool IsHallington { get { return MatchformId == 3; } }
        public bool Official { get; set; }
        public bool Shootout { get; set; }
        public string Remarks { get; set; }
        public byte[] MatchRowversion { get; set; }
    }
}