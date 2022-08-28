using System;
using System.Globalization;
using PetaPoco;

namespace Caddie.Data.Dto
{
    [TableName("ms.UmbracoLog")]
    [ExplicitColumns]
    public class UmbracoLog
    {
        [Column("TourDate")]
        public string Type { get; set; }
        private string dateString;
        [Column("TourDate")]
        public string DateString
        {
            get { return Date.ToString("dddd d MMM", new CultureInfo("DA")); }
            set { this.dateString = value; }
        }
        [Column("TourDate")]
        public DateTime Date { get; set; }

        [Column("TourDate")]
        public string Body { get; set; }
    }
}
