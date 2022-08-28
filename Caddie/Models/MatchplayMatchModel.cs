using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caddie.Models
{
    public class MatchplayMatchModel
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int Playround { get; set; }
        public int Season { get; set; }
        public int MatchResult { get; set; }
        public string ResultText { get; set; }
        public string ResultTextFull
        {
            get
            {
                switch (MatchResult)
                {
                    case 1:
                    case 2:
                        return ResultText;
                    default:
                        return "Ikke spillet";
                }
            }
        }
        public string TeamName1 { get; set; }
        public int LeagueTeamId1 { get; set; }
        public string TeamName2 { get; set; }
        public int LeagueTeamId2 { get; set; }
        public string MatchText {
            get {
                return string.Format("{0} - {1}", TeamName1, TeamName2);
            }
        }
        public string HtmlMatchText
        {
            get
            {
                switch (MatchResult)
                {
                    case 1:
                        return string.Format("<b>{0}</b> - {1}", TeamName1, TeamName2);
                    case 2:
                        return string.Format("{0} - <b>{1}</b>", TeamName1, TeamName2);
                    default:
                        return string.Format("{0} - {1}", TeamName1, TeamName2);
                }
            }
        }
    }

}
