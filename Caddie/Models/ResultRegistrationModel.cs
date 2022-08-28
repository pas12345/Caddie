using System.Globalization;

namespace Caddie.Models
{
    public class ResultRegistrationModel
    {
        public ResultRegistrationModel() {;}
        public int Id { get; set; }
        public int MemberShipId { get; set; }
        public int MatchId { get; set; }
        public int MatchformId { get; set; }
        public int VgcNo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.Firstname))
                    return this.Lastname;
                if (string.IsNullOrEmpty(this.Lastname))
                    return this.Firstname;

                return string.Format(CultureInfo.InstalledUICulture, "{0}, {1}", 
                    this.Firstname.Trim(), this.Lastname);
            }
        }
        public string FullName2
        {
            get
            {
                if (string.IsNullOrEmpty(this.Firstname))
                    return this.Lastname;
                if (string.IsNullOrEmpty(this.Lastname))
                    return this.Firstname;

                return string.Format(CultureInfo.InstalledUICulture, "{0}, {1}", 
                    this.Lastname.Trim(), this.Firstname);
            }
        }
        public bool Dining { get; set; }
        public int HcpGroup { get; set; }
        public int Hcp { get; set; }
        public int MaxA { get; set; }
        public int MaxB { get; set; }
        public decimal HcpIndex { get; set; }
        public int? Brutto { get; set; }
        public int? Netto { get; set; }
        public int? Puts { get; set; }
        public int? Points { get; set; }
        public int? DamstahlPoints { get; set; }
        public int? Hallington { get; set; }
        public int? Birdies { get; set; }
        public int? Shootout { get; set; }
        public int Rank { get; set; }
        public bool Official { get; set; }
        public int No { get; set; }
        public bool IsStrokePlay { get; set; }
        public bool IsHallington { get; set; }
        public byte[] Rowversion { get; set; }
    }
}