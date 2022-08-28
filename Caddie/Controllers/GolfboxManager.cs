using System;
using System.Collections.Generic;
using System.Linq;


namespace Caddie.Controllers
{
    class GolfboxManager
    {
        public Dictionary<int, decimal> GetHandicaps(string account, 
                                                    string username, 
                                                    string password, 
                                                    System.Guid groupId)
        {
            Dictionary < int, decimal> hcps = new Dictionary<int, decimal>();

            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException("username");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");
            if (groupId == null)
                throw new ArgumentNullException("groupGuid");

            Golfbox.GroupMember[] members;
            Golfbox.GroupClient proxy = new Golfbox.GroupClient();

            Golfbox.UserCredentials uc = new Golfbox.UserCredentials();
            uc.Account = account;
            uc.Username = username;
            uc.Password = password;

            Golfbox.GroupNameList groups
                = proxy.GetGroupMembers(uc, groupId);

            if (groups.Items.GetLength(0) < 1)
                throw new ApplicationException("Failed to find group with id: " + groupId.ToString());

            members = groups.Items[0].Members;
            for (int i = 0; i < members.GetLength(0); i++)
            {
                if (members[i].Sex.Equals("m", StringComparison.OrdinalIgnoreCase))
                {
                    decimal hcp;
                    decimal.TryParse(members[i].Handicap, out hcp);
                    int vgcno;
                    int.TryParse(members[i].MemberNumber, out vgcno);

                    hcps.Add(vgcno, hcp);
                }
            }
            return hcps;
        }

    }
}
