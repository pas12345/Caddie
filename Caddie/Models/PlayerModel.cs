using System;
using System.Globalization;

namespace Caddie.Models
{
    public class PlayerModel
    {
        public PlayerModel() {; }
        public int Id { get; set; }
        public int VgcNo { get; set; }
        public int MemberShipId { get; set; }
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

                return string.Format(CultureInfo.InstalledUICulture, "{0}, {1}", this.Lastname.Trim(), this.Firstname);
            }
        }
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int NameGroup { get; set; }
        public int Season { get; set; }
        public bool Sponsor { get; set; }
        public decimal HcpIndex { get; set; }
        public DateTime HcpUpdated { get; set; }
    }
}
