
using System.Globalization;

namespace Caddie.Models
{
    public class NearestPinResultModel
    {
        public int Id { get; set; }
        public int MembershipId { get; set; }
        public int MatchId { get; set; }
        public int VgcNo { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(Firstname))
                    return Lastname;
                if (string.IsNullOrEmpty(Lastname))
                    return Firstname;

                return string.Format(CultureInfo.InstalledUICulture, "{0}, {1}",
                    this.Firstname.Trim(), this.Lastname);
            }
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PinName { get; set; }
        public int DistanceInCM { get; set; }
    }
}