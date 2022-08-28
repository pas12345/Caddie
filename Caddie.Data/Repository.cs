using System;
using System.Collections.Generic;
using System.Linq;
using PetaPoco;
using System.Data.SqlClient;

namespace Caddie.Data
{
    public class Repository
    {

        #region Method: Competitions
        public IEnumerable<Dto.ListEntry> GetCompetitions()
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.ListEntry>("SELECT [CompetitionId] as [Key] " +
                ",[CompetitionText] as [Value] " +
                "FROM[ms].[Competition] " +
                "where Active = 1 " +
                "order by listorder");

            return list;
        }
        public IEnumerable<Dto.CompetitionResult> GetCompetitionResults(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.CompetitionResult>(";exec [ms].[CompetitionResults] @MatchId"
                    , new { MatchId = matchId});
            return list;
        }
        public Dto.CompetitionResult GetCompetitionResult(int matchId, int competitionId)
        {
            MSDatabase.EnableAutoSelect = false;
            var dto = MSDatabase.Query<Dto.CompetitionResult>("SELECT [MatchDate],[CompetitionText] " +
                ",[CompetitionResultId],[MembershipId],[CompetitionId] " +
                ",[VgcNo],[Firstname],[Lastname],[MatchId] " +
                " FROM [ms].[vCompetitionResult]" +
                " WHERE MatchId = @MatchId  and CompetitionId = @CompetitionId " +
                " order by listorder"
                    , new
                    {
                        MatchId = matchId,
                        CompetitionId = competitionId
                    }).FirstOrDefault();

            return dto;
        }
        public Dto.CompetitionResult GetCompetitionResultById(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            var dto = MSDatabase.Query<Dto.CompetitionResult>("SELECT [MatchDate],[CompetitionText] " +
                ",[CompetitionResultId],[MembershipId],[CompetitionId] " +
                ",[VgcNo],[Firstname],[Lastname],[MatchId] " +
                " FROM [ms].[vCompetitionResult]" +
                " WHERE CompetitionResultId = @CompetitionResultId "
                    , new
                    {
                        CompetitionResultId = id
                    }).FirstOrDefault();

            return dto;
        }
        /*
        create PROCEDURE [ms].[CompetitionResultUpsert]
	        @MembershipId int, 
	        @MatchId int, 
	        @CompetitionId int, 
	        @CompetitionResultId int output 
        */
        public Data.Dto.CompetitionResult UpdateCompetitionResult(Data.Dto.CompetitionResult p)
        {
            MSDatabase.EnableAutoSelect = false;
            var id = new SqlParameter("NearestPinId", System.Data.SqlDbType.Int);
            id.Direction = System.Data.ParameterDirection.InputOutput;
            id.Value = p.Id;

            var result = MSDatabase.Execute(";exec [ms].[CompetitionResultUpsert] @MembershipId, " +
                    "@MatchId,@CompetitionId,@CompetitionResultId output"
                    , new
                    {
                        MembershipId = p.MembershipId,
                        MatchId = p.MatchId,
                        CompetitionId = p.CompetitionId,
                        CompetitionResultId = id
                    });
            if (p.Id < 0)
                p.Id = (int)id.Value;
            return p;
        }
        #endregion

        #region NearestPin

        public Dto.NearestPinResult GetNearestPinResult(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.NearestPinResult>("SELECT [VgcNo],[Firstname],[Lastname],[NearestPinId]" +
                ",[MemberShipId],[MatchId],[PinName],[CourseName],[DistanceInCm],[MatchDate] " +
                " FROM [ms].[vNearestPin]" +
                " where NearestPinId = @NearestPinId",
                new { NearestPinId = id }).FirstOrDefault();
        }
        public List<Dto.NearestPinResult> GetNearestPinResults(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.NearestPinResult>("[ms].MatchResultNearestPin @MatchId",
                new { MatchId = matchId }).ToList();
        }

        /*
        create PROCEDURE [ms].[NearestPinUpsert]
	        @MembershipId int,
	        @MatchId int,
	        @PinName varchar(100),
	        @DistanceInCm int,
	        @NearestPinId int OUTPUT
        */
        public Data.Dto.NearestPinResult UpdateNearestPinResult(Data.Dto.NearestPinResult p)
        {
            MSDatabase.EnableAutoSelect = false;
            var NearestPinId = new SqlParameter("NearestPinId", System.Data.SqlDbType.Int);
            NearestPinId.Direction = System.Data.ParameterDirection.InputOutput;
            NearestPinId.Value = p.Id;

            var result = MSDatabase.Execute(";exec [ms].[NearestPinUpsert] @MembershipId, " +
                    "@MatchId,@PinName,@DistanceInCm, @NearestPinId output"
                    , new
                    {
                        NearestPinId = NearestPinId,
                        MatchId = p.MatchId,
                        MembershipId = p.MembershipId,
                        PinName = p.PinName,
                        DistanceInCm = p.DistanceInCM
                    });
            if (NearestPinId.Value != null)
                p.Id = (int)NearestPinId.Value;
            return p;
        }

        #endregion        

        #region Method: Match
        public Dto.Match GetNextMatch(int season)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.Match>("select top(1) " +
                "[MatchId],[MatchDate],[MatchForm],[MatchText],[Sponsor],[SponsorLogoId],[CourseName]" +
                ",[Par],[Tee],[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]" +
                ", [CourseDetailId],[MatchRowversion],[Shootout] " +
                " from [ms].[fnMatchSeason] (@Season) where MatchDate >= @MatchDate order by MatchDate asc",
                    new { MatchDate = today, Season = season });

            return list.FirstOrDefault();
        }
        public IEnumerable<Dto.Match> GetSeasonMatches()
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.Match>("select " +
                "[MatchId],[MatchDate],[MatchForm],[MatchText],[Sponsor],[SponsorLogoId],[CourseName]" +
                ",[Par],[Tee],[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]" +
                ", [CourseDetailId],[MatchRowversion],[Shootout] " +
                " from [ms].[vMatchSeason] order by MatchDate");
            return list;
        }
        public List<Dto.ListDateTimeEntry> GetMatchesBefore(DateTime beforeDate)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListDateTimeEntry>("SELECT [MatchId] as [Key], [MatchDate] as [Value] " +
                    "FROM [ms].[vMatchSeason] " +
                    "where [MatchDate] <= @BeforeDate " +
                    "order by [MatchDate] desc",
                    new { BeforeDate = beforeDate }).ToList();
        }

        public List<Dto.ListDateTimeEntry> GetMatchesAfter(DateTime afterDate)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListDateTimeEntry>("SELECT [MatchId] as [Key], [MatchDate] as [Value] " +
                    "FROM [ms].[vMatchSeason] " +
                    "where [MatchDate] >= @AfterDate " +
                    "order by [MatchDate] asc",
                    new { AfterDate = afterDate }).ToList();
        }

        public Dto.Match GetMatch(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var match = MSDatabase.Query<Dto.Match>("select  " +
                "[MatchId],[MatchDate],[MatchForm],[MatchText],[Sponsor],[SponsorLogoId],[CourseName]" +
                ",[Par],[Tee],[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]" +
                ", [CourseDetailId],[MatchRowversion],[Shootout] " +
                " from [ms].[vMatch] where MatchId = @MatchId",
                new { MatchId = matchId }).FirstOrDefault();

            return match;
        }
        public Dto.Match GetLastMatch()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(1);
            MSDatabase.EnableAutoSelect = false;
            var match = MSDatabase.First<Dto.Match>("select top(1) " +
                "[MatchId],[MatchDate],[MatchForm],[MatchText],[Sponsor],[SponsorLogoId],[CourseName]" +
                ",[Par],[Tee],[CourseRating],[Slope],[Remarks],[Official],[ClubName],[MatchformId]" +
                ", [CourseDetailId],[MatchRowversion],[Shootout] " +
                " from [ms].[vMatch] where MatchDate < @MatchDate " +
                " order by MatchDate desc",
                new { MatchDate = dt });

            return match;
        }
        public List<Dto.ListDateTimeEntry> GetMatchDates(DateTime start, DateTime end)
        {
            DateTime d = DateTime.Now;
            d = d.AddHours(22 - d.Hour); // kl 22 idag
            end = end > d ? d : end;

            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListDateTimeEntry>("SELECT distinct MatchId as [Key], MatchDate as [Value]" +
                                                        " FROM  [ms].[vMatchSeason] " +
                                                        " ORDER BY MatchDate desc").ToList();
        }
        public List<Dto.ListEntry> GetMatchforms()
        {
            DateTime d = DateTime.Now;
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListEntry>("SELECT distinct MatchformId as [Key], Matchform as [Value]" +
                                                        " FROM  [ms].[Matchform] " +
                                                        " ORDER BY Matchform").ToList();
        }

        public Data.Dto.Match UpdateMatch(Data.Dto.Match m)
        {
            MSDatabase.EnableAutoSelect = false;
            SqlParameter matchId = new SqlParameter("MatchId", System.Data.SqlDbType.Int);
            matchId.Direction = System.Data.ParameterDirection.InputOutput;
            matchId.Value = m.Id;

            var match = MSDatabase.Execute(";exec [ms].[MatchUpsert] @MatchId output, " +
                    "@MatchDate, @MatchformId, @CourseDetailId, @Par, @Description, @Sponsor, " +
                    "@SponsorLogoId, @Remarks, @Official,@Shootout, @timestamp "
                    , new
                    {
                        MatchId = matchId,
                        @MatchDate = m.MatchDate,
                        @MatchformId = m.MatchformId,
                        @CourseDetailId = m.CourseDetailId,
                        @Par = m.Par,
                        @Description = m.MatchText,
                        @Sponsor = m.Sponsor,
                        @SponsorLogoId = m.SponsorLogoId,
                        @Remarks = m.Remarks,
                        @Official = m.Official,
                        @Shootout = m.Shootout,
                        @timestamp = m.MatchRowversion
                    });
            m.Id = (int)matchId.Value;
            return m;
        }


        #endregion

        #region Method: Player
        public IEnumerable<Dto.Player> GetPlayers(int season)
        {
            DateTime start = new DateTime(season, 1, 1);
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Player>("SELECT [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
                "[Sponsor],[Phone],[CellPhone],[HcpIndex],[Fee],[Insurance],[Season],[Eclectic],[HcpUpdated]," +
                "[LastUpdate],[PlayerId],[MemberShipId], [NameGroup] " +
                "FROM ms.vPlayer " +
                "WHERE [Season]=@Season " +
                "ORDER BY [NameGroup], [LastName]",
                new { Season = season });
        }
        public Dto.Player GetPlayer(int vgcNo)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.SingleOrDefault<Dto.Player>("SELECT Top(1) [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
                "[Sponsor],[Phone],[CellPhone],[HcpIndex],[HcpUpdated]," +
                "[LastUpdate],[PlayerId] " +
                "FROM ms.Player where vgcNo=@VgcNo",
                new { VgcNo = vgcNo});
        }
        public Dto.Player GetPlayer(int vgcNo, int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.SingleOrDefault<Dto.Player>("SELECT [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
                "[Sponsor],[Phone],[CellPhone],[HcpIndex],[Fee],[Insurance],[Season],[Eclectic],[HcpUpdated]," +
                "[IsMale],[PlayerId],[MemberShipId]" +
                "FROM ms.vPlayer where VgcNo = @VgcNo and season = @Season ",
                new { VgcNo = vgcNo, @Season = season });
        }
        public bool UpdatePlayer(Data.Dto.Player p)
        {
            MSDatabase.EnableAutoSelect = false;

            var result = MSDatabase.Execute(";EXEC [ms].[PlayerUpsert] @VgcNo, @Firstname" +
                    ", @Lastname, @ZipCode, @City, @Address, @Email, @Sponsor, @Phone, @HcpIndex"
                    , new { VgcNo = p.VgcNo,
                            Firstname = p.Firstname,
                            Lastname = p.Lastname,
                            ZipCode = p.ZipCode,
                            City = p.City,
                            Address = p.Address,
                            Email = p.Email,
                            Sponsor = p.Sponsor,
                            Phone = p.Phone,
                            HcpIndex = p.HcpIndex
                    });
            return true;
        }
        public int UpdateMembership(int vgcNo, int season)
        {
            MSDatabase.EnableAutoSelect = false;
            var id = new SqlParameter("MemberShipId", System.Data.SqlDbType.Int);
            id.Direction = System.Data.ParameterDirection.Output;
            id.Value = -1;
            var result = MSDatabase.Execute(";EXEC [ms].[MemberShipUpsert] @MemberShipId output, @VgcNo, @Season"
                    , new { MemberShipId = id,  VgcNo = vgcNo, Season = season});

            int i = -1;
            if (id.Value != null)
                i = (int)id.Value;
            return i;
        }

        /// <summary>
        /// Only for test
        /// </summary>
        /// <param name="vgcNo"></param>
        /// <returns></returns>
        public bool DeletePlayer(int vgcNo)
        {
            MSDatabase.EnableAutoSelect = false;

            var result = MSDatabase.Execute(";delete [ms].[Player] where VgcNo=@VgcNo"
                    , new { VgcNo = vgcNo });
            return true;
        }
        /// <summary>
        /// Only for test
        /// </summary>
        /// <param name="vgcNo"></param>
        /// <returns></returns>
        public bool DeleteMembership(int vgcNo, int season)
        {
            MSDatabase.EnableAutoSelect = false;

            var result = MSDatabase.Execute(";delete [ms].[MemberShip] where VgcNo=@VgcNo and Season = @Season"
                    , new { VgcNo = vgcNo, Season = season});
            return true;
        }
        public IEnumerable<Dto.Player> GetMemberNames(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Player>("Select MemberShipId, FirstName, LastName, ms.PlayerNameGroup(LastName) as [NameGroup] " +
                                                "FROM ms.vPlayer " +
                                                "where Season=@Season " +
                                                "ORDER BY LastName",
                new { Season = season });
        }

        public IEnumerable<Dto.Player> GetNonMemberNames(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Player>("SELECT VgcNo, Firstname, Lastname " +
                                                "FROM ms.Player as m where not exists  " +
                                                "(select MemberShipId from ms.MemberShip as ms " +
                                                "WHERE m.VgcNo = ms.VgcNo AND Season = @Season) " +
                                                "ORDER BY LastName",
                new { Season = season });
        }

        public Dto.Player GetMember(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.SingleOrDefault<Dto.Player>("SELECT Top(1) [VgcNo],[FirstName],[LastName],[ZipCode],[City],[Address],[Email]," +
                "[Sponsor],[Phone],[CellPhone],[HcpIndex],[Fee],[Insurance],[Season],[Eclectic],[HcpUpdated]," +
                "[LastUpdate],[IsMale],[PlayerId],[MemberShipId]" +
                "FROM ms.vPlayer where MemberShipId=@Id",
                new { Id = id });
        }
        /// <summary>
        /// Get all player names not registrered as members in the season
        /// </summary>
        /// <param name="season"></param>
        /// <returns></returns>
        public IEnumerable<Dto.Player> GetAllNonMemberNames(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Player>("Select VgcNo, FirstName, LastName " +
                                            "FROM ms.vPlayer " +
                                            "Where vgcno not in " +
                                            "(select vgcno from ms.MemberShip where Season=@Season )",
                new { Season = season });
        }
        public IEnumerable<Dto.Membership> GetAllMembers(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Membership>(";exec [ms].[MembershipSelectAll] @Season"
                    , new { Season = season});
        }
        public IEnumerable<Dto.Membership> GetMemberships(int season, int part)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Membership>(";exec [ms].[MembershipSelectSeason] @Season, @Part"
                    , new { Season = season, Part = part });
        }

        public int UpdateHcp(int vgcNo, decimal hcpIndex)
        {
            hcpIndex = hcpIndex > 63 ? 48 : hcpIndex;
            MSDatabase.EnableAutoSelect = false;

            int result = MSDatabase.Execute(";EXEC [ms].[PlayerUpdateHcp] @VgcNo, @HcpIndex"
                    , new { VgcNo = vgcNo, @HcpIndex = hcpIndex });
            return result;
        }

        #endregion

        #region Method: MatchResults

        public IEnumerable<Dto.ListDateTimeEntry> GetMatchResultDates()
        {
            return GetMatchResultDates(SeasonStart, SeasonStart);
        }

        public List<Dto.ListDateTimeEntry> GetMatchResultDates(DateTime startDate, DateTime endDate)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListDateTimeEntry>(";exec [ms].[MatchResultSelectDates] @StartDate, @EndDate",
                new { @StartDate = startDate, @EndDate = endDate }).ToList();
        }


        public Dto.ResultMatch GetLastResult()
        {
            MSDatabase.EnableAutoSelect = false;
            
            var list = MSDatabase.Query<Dto.ResultMatch>("SELECT TOP (1) FirstName, LastName, Brutto, Netto, " +
                "DamstahlPoints, Points, Hallington, Tee, MatchFormId, OverallWinner, " +
                "MatchDate, MatchResultId, MatchId, HcpIndex, Hcp, Dining, " +
                "Puts, Birdies, [Rank], Official, VgcNo, ClubName, CourseName " +
                "from [ms].[vMatchResult]	" +
                "WHERE ([OverallWinner] = 1) " +
                "ORDER BY MatchDate DESC");

            return list.FirstOrDefault();
        }

        public Data.Dto.ResultMatch GetMatchResult(int matchResultId)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ResultMatch>("SELECT VgcNo, MemberShipId, " +
                    "MatchId, HcpGroup, Hcp, OverallWinner, " +
                    "HcpIndex, FirstName, LastName, Matchform, Par, " +
                    "MatchResultId, Puts, Brutto,Netto, DamStahlPoints, Hallington, " +
                    "Points, Birdies, ShootOut, Rank, Dining, [timestamp] " +
                "from ms.vMatchResult where MatchResultId = @Id",
                new { Id = matchResultId }).FirstOrDefault();
        }
        public List<Data.Dto.ResultMatch> GetMatchResults(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ResultMatch>(";exec [ms].[MatchResultSelectWinners] @matchId",
                new { @matchId = matchId}).ToList();
        }
        public IEnumerable<Dto.Membership> GetMatchResults(DateTime start, DateTime end)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.Membership>("SELECT distinct  r.VgcNo, r.Firstname, r.Lastname" +
                    " FROM     ms.vMatchResult r" +
                    " WHERE  r.MAtchDate between @StartDate and @EndDate " +
                    " ORDER BY r.Lastname",
                new { StartDate = start, EndDate = end });
        }

        public IEnumerable<Dto.ResultMatch> GetMatchResults(DateTime start, DateTime end, int vgcNo)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.ResultMatch>("SELECT VgcNo, Firstname, Lastname, MatchDate, Brutto, Hcp, Hallington, " +
                                                "HcpIndex, Netto, Points, Shootout, OverallWinner, MatchId, " +
                                                "DamstahlPoints, Birdies, Puts, CourseName, ClubName, Official " +
                                                "FROM   ms.vMatchResult  " +
                                                "WHERE VgcNo=@VgcNo and MatchDate between @Start and @End " +
                                                "ORDER BY MatchDate desc",
                    new { Start = start, End = end, VgcNo = vgcNo });
            return list;
        }
        // skal jeg smide den her ud ?
        public List<Data.Dto.ResultMatch> GetMatchResultWinners(int matchId, int maxA)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ResultMatch>(";exec [ms].[MatchResultSelectWinners] @EventId, @MaxA"
                    , new { EventId = matchId, @MaxA = maxA}).ToList();
        }

        public Data.Dto.ResultMatch GetMatchResultForRegistration(int matchId, int vgcNo)
        {
            MSDatabase.EnableAutoSelect = false;
            Data.Dto.ResultMatch m = MSDatabase.Query<Dto.ResultMatch>(";exec [ms].[MatchResultForMemberRegistration] @MatchId, @VgcNo",
                new { MatchId = matchId, VgcNo = vgcNo }).FirstOrDefault();
            return m;
        }

        public List<Data.Dto.ResultMatch> GetMatchResultListForRegistration(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ResultMatch>(";exec [ms].[MatchResultListForRegistration] @MatchId",
                new { MatchId = matchId }).ToList();
        }


        public Data.Dto.ResultMatch UpdateMatchResult(Data.Dto.ResultMatch p)
        {
            MSDatabase.EnableAutoSelect = false;
            SqlParameter resultId = new SqlParameter("MatchResultId", System.Data.SqlDbType.Int);
            resultId.Direction = System.Data.ParameterDirection.InputOutput;
            resultId.Value = p.Id;

            var result = MSDatabase.Execute(";exec [ms].[MatchResultUpsert] @MatchResultId output, " +
                    "@VgcNo,@MatchId,@HcpIndex,@Hcp,@Puts,@Brutto" +
                    ",@Points,@Hallington,@Birdies,@ShootOut,@HcpGroup,@Dining,@timestamp"
                    , new
                    {
                        MatchResultId = resultId,
                        VgcNo = p.VgcNo,
                        MatchId = p.MatchId
                        ,
                        Hcp = p.Hcp,
                        HcpIndex = p.HcpIndex,
                        Puts = p.Puts,
                        Brutto = p.Brutto
                        ,
                        Points = p.Points,
                        Hallington = p.Hallington,
                        Birdies = p.Birdies
                        ,
                        ShootOut = p.ShootOut,
                        HcpGroup = p.HcpGroup
                        ,
                        Dining = p.Dining,
                        timestamp = p.Rowversion
                    });
            p.Id = resultId.Value == DBNull.Value ? 0 :(int)resultId.Value;
            return p;
        }
        public int DeleteMatchResult(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Execute("delete [ms].[MatchResult] where MatchResultId = @Id",
                    new { Id = id });
        }

        public int MatchResultSettleByStroke(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var result = MSDatabase.Execute(";exec [ms].[MatchResultSettleByStroke] @MatchId",
                new { MatchId = matchId });
            return 0;
        }

        public int MatchResultSettleByHallington(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var result = MSDatabase.Execute(";exec [ms].[MatchResultSettleByHallington] @MatchId",
                new { MatchId = matchId });
            return 0;
        }

        public int MatchResultSettleByPoints(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var result = MSDatabase.Execute(";exec [ms].[MatchResultSettleByPoints] @MatchId",
                new { MatchId = matchId });
            return 0;
        }

        public int MatchResultSetDamstahlPoints(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var result = MSDatabase.Execute(";exec [ms].[MatchResultSetDamstahlPoints] @MatchId",
                new { MatchId = matchId });
            return 0;
        }

        #endregion 

        #region Method: MatchPlay

        public IEnumerable<Dto.Player> GetMatchPlayerList(DateTime start, DateTime end)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.Player>("SELECT distinct  VgcNo, Firstname, Lastname " +
                                                       "FROM ms.vMatchResult " +
                                                       "WHERE MatchDate between @Start and @End " +
                                                       "ORDER BY Lastname desc",
                    new { Start = start, End = end });
            return list.ToList();
        }

        #endregion

        #region Ranking
        public List<Dto.RankItemBrutto> GetBruttoRanking(DateTime startDate, DateTime endDate, int cnt)
        {
            var query = MSDatabase.Fetch<Dto.RankItemBrutto>(";EXEC ms.[RankBrutto] @StartDate, @EndDate, @Cnt",
                new { StartDate = startDate, EndDate = endDate, Cnt = cnt });

            return query;
        }
        public List<Dto.RankItemHallington> GetHallingtonRanking(DateTime startDate, DateTime endDate, int cnt)
        {
            var query = MSDatabase.Fetch<Dto.RankItemHallington>(";EXEC ms.[RankHallington] @StartDate, @EndDate, @Cnt",
                new { StartDate = startDate, EndDate = endDate, Cnt = cnt });

            return query;
        }
        public List<Dto.RankItemNetto> GetNettoRanking(DateTime startDate, DateTime endDate, int cnt)
        {
            var query = MSDatabase.Fetch<Dto.RankItemNetto>(";EXEC ms.[RankNetto] @StartDate, @EndDate, @Cnt",
                new { StartDate = startDate, EndDate = endDate, Cnt = cnt });

            return query;
        }
        public List<Dto.RankItemPuts> GetPutsRanking(DateTime startDate, DateTime endDate, int cnt)
        {
            var query = MSDatabase.Fetch<Dto.RankItemPuts>(";EXEC ms.[RankPuts] @StartDate, @EndDate, @Cnt",
                new { StartDate = startDate, EndDate = endDate, Cnt = cnt });

            return query;
        }
        public List<Dto.RankItemBirdie> GetBirdieRanking(DateTime startDate, DateTime endDate, int cnt)
        {
            var query = MSDatabase.Fetch<Dto.RankItemBirdie>(";EXEC ms.[RankBirdies] @StartDate, @EndDate, @Cnt",
                new { StartDate = startDate, EndDate = endDate, Cnt = cnt });

            return query;
        }
        public List<Dto.RankItemDamstahl> GetDamstahlRanking(DateTime startDate, DateTime endDate)
        {
            var query = MSDatabase.Fetch<Dto.RankItemDamstahl>(";EXEC ms.[RankDamstahl] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = endDate });

            return query;
        }

        public List<Dto.RankItemStableford> GetStablefordRanking(int season)
        {
            DateTime startDate = new DateTime(season, 1, 1);
            var query = MSDatabase.Fetch<Dto.RankItemStableford>(";EXEC ms.[RankStableford] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = startDate.AddYears(1)});

            return query;
        }

        public List<Dto.RankItemStableford> GetSimulatorRanking(DateTime startDate, DateTime endDate)
        {
            var query = MSDatabase.Fetch<Dto.RankItemStableford>(";EXEC ms.[RankSimulator] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = endDate });

            return query;
        }

        public List<Dto.RankItemShootout> GetShootoutRanking(DateTime startDate, DateTime endDate)
        {
            var query = MSDatabase.Fetch<Dto.RankItemShootout>(";EXEC ms.[RankShootout] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = endDate });

            return query;
        }
        public List<Dto.RankItemNearestPin> GetNearestPinRanking(DateTime startDate, DateTime endDate)
        {
            var query = MSDatabase.Fetch<Dto.RankItemNearestPin>(";EXEC ms.[RankNearestPin] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = endDate });

            return query;
        }
        public List<Dto.RankItemOverallWinner> GetOverallRanking(DateTime startDate, DateTime endDate)
        {
           var query = MSDatabase.Fetch<Dto.RankItemOverallWinner>(";EXEC ms.[RankOverall] @StartDate, @EndDate",
                new { StartDate = startDate, EndDate = endDate });

            return query;
        }
        public IEnumerable<Dto.RankItemOther> GetCompetitionResult(int matchId)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.RankItemOther>("SELECT [CompetitionResultId],[MatchDate],[CompetitionText],[VgcNo]" +
                " ,[Firstname],[Lastname] FROM [ms].[vCompetitionResult] where MatchId = @MatchId",
                 new { MatchId = matchId });

            return list;
        }
        #endregion

        #region MatchPlay
        public IEnumerable<Dto.LeagueTeam> MatchPlayTeamList(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueId],[LeagueName],[LeagueTeamId],[TeamName],[VgcNo],[VgcNoPartner] " +
                                "FROM [ms].[vLeagueTeam] where Season = @Season order by VgcNo",
                    new { Season= season });

            return list;

        }
        public int MatchPlayTeamUpdate(Dto.LeagueTeam team)
        {
            MSDatabase.EnableAutoSelect = false;
            if (team.IsSingle)
            {
                var result = MSDatabase.Execute(";exec [ms].[LeagueTeamUpsert] @LeagueId,  @VgcNo"
                        , new { LeagueId = team.LeagueId, VgcNo = team.VgcNo});
                return result;
            }
            else
            {
                var result = MSDatabase.Execute(";exec [ms].[LeagueTeamDoubleUpsert] @LeagueId, @VgcNo, @VgcNoPartner"
                        , new { LeagueId = team.LeagueId, VgcNo = team.VgcNo, VgcNoPartner = team.VgcNoPartner });
                return result;
            }
        }
        public int MatchPlayMatchUpdate(Dto.LeagueMatch m)
        {
            MSDatabase.EnableAutoSelect = false;
            var result = MSDatabase.Execute(";EXECUTE [ms].[MatchplayMatchUpsert] " +
                    "@LeagueMatchId, @LeagueId, @Playround, @LeagueTeamId1, @LeagueTeamId2;",
                    new { @LeagueMatchId = m.LeagueMatchId, @LeagueId = m.LeagueId, @Playround = m.Playround,
                        @LeagueTeamId1 = m.LeagueTeamId1, @LeagueTeamId2 = m.LeagueTeamId2 });
            return result;
        }

        public int MatchPlayDeleteSeasonTeams(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Execute("delete [ms].[LeagueTeam] where Season = @Season",
                    new { Season = season });
        }

        public Dto.LeagueMatch GetMatchplayMatch(int matchId)
        {
            var list = MSDatabase.Query<Dto.LeagueMatch>("SELECT LeagueId, LeagueName, Playround, LeagueMatchId, " +
                            "MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 " +
                            "from ms.vLeagueMatch " +
                            "where LeagueMatchId = @matchId",
                             new { matchId = matchId });
            return list.FirstOrDefault();
        }

        public IEnumerable<Dto.LeagueMatch> GetMatchplayMatchList(int leagueId)
        {
            var list = MSDatabase.Query<Dto.LeagueMatch>("SELECT LeagueId, LeagueName, Playround, LeagueMatchId, " +
                            "MatchResult, ResultText, TeamName1, TeamName2, LeagueTeamId1, LeagueTeamId2 " +
                            "from ms.vLeagueMatch " +
                            "where LeagueId = @LeagueId " +
                            "order by Playround desc, LastUpdate desc",
                             new { LeagueId = leagueId });
            return list;
        }
        public int UpdateMatchplayResult(Dto.LeagueMatch dto)
        {
            MSDatabase.EnableAutoSelect = false;
                var result = MSDatabase.Execute(";UPDATE [ms].[LeagueMatch]" +
                    " SET [MatchResult] = @matchResult,[ResultText] = @resultText, [LastUpdate] = Getdate()" +
                    " WHERE LeagueMatchId = @matchId"
                    , new { matchId = dto.LeagueMatchId, matchResult = dto.MatchResult,
                        resultText = dto.ResultText });
                return result;
        }
        public IEnumerable<Dto.LeagueTeam> GetMatchplayTeams(int leagueId)
        {
            int season = DateTime.Now.Year;
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
                            " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
                            " FROM [ms].[LeagueTeam] " +
                            " where [LeagueId] = @LeagueId and [Season] = @Season" +
                            " order by TeamName",
                             new { LeagueId = leagueId, Season = season });
            return list;
        }
        public Dto.LeagueTeam MatchplayTeamExists(int vgcNo)
        {
            int season = DateTime.Now.Year;
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
                            " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
                            " FROM [ms].[LeagueTeam] " +
                            " where [LeagueId] < 3 and [Season] = @Season and [VgcNo] = @VgcNo" +
                            " order by TeamName",
                             new { VgcNo = vgcNo, Season = season });
            return list.FirstOrDefault();
        }
        public Dto.LeagueTeam MatchplayTeamExists(int vgcNo, int vgcNoPartner)
        {
            int season = DateTime.Now.Year;
            if (vgcNo > vgcNoPartner)
            {
                int i = vgcNo;
                vgcNo = vgcNoPartner;
                vgcNoPartner = i;
            }

            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
                            " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
                            " FROM [ms].[LeagueTeam] " +
                            " where [LeagueId] = 3 and [Season] = @Season and [VgcNo] = @VgcNo" +
                            " and [VgcNoPartner] = @VgcNoPartner" +
                            " order by TeamName",
                             new { VgcNo = vgcNo, VgcNoPartner = vgcNoPartner, Season = season });
            return list.FirstOrDefault();
        }
        public Dto.LeagueTeam MatchplayGetTeam(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.LeagueTeam>("SELECT [LeagueTeamId],[TeamName]," +
                            " [VgcNo],[VgcNoPartner],[Season],[LeagueId] " +
                            " FROM [ms].[LeagueTeam] " +
                            " where [LeagueTeamId] = @Id",
                             new { Id = id});
            return list.FirstOrDefault();
        }
        public int MatchplayDeleteTeam(int id)
        {
            MSDatabase.EnableAutoSelect = false;
            var res = MSDatabase.Execute(";delete [ms].[LeagueTeam] " +
                            " where [LeagueTeamId] = @Id",
                             new { Id = id });
            return 0;
        }

        public List<KeyValuePair<int, String>> GetMatchPlayLeagueList()
        {
            return new List<KeyValuePair<int, String>>()
                {
                    new KeyValuePair<int, String>(1, "Single A"),
                    new KeyValuePair<int, String>(2, "Single B"),
                    new KeyValuePair<int, String>(3, "Par")
                };
        }
        #endregion

        #region GetAllLeagues
        public IEnumerable<Dto.ListEntry> GetAllLeagues(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.ListEntry>("SELECT LeagueId, LeagueName from League" +
                " where Season=@Season order by LeagueId",
                new { Season = season });
        }
        #endregion
        
        #region GetMathcPlayMatchList
        public IEnumerable<Dto.LeagueMatch> GetMatchPlayList(int season)
        {
            MSDatabase.EnableAutoSelect = false;
            return MSDatabase.Query<Dto.LeagueMatch>("SELECT LeagueId, LeagueName, Playround, " +
                    "Season, LeagueMatchId, MatchResult, ResultText, TeamName1, TeamName2 " +
                    "from ms.vLeagueMatch  where Season=@Season and Playround > 0 " +
                    "order by Playround desc, ResultText desc, Lastupdate",
                new { Season = season });

        }
        #endregion

        #region GetLatestMatchPlayMatch
        public IEnumerable<Dto.LeagueMatch> GetLatestMatchPlay(int season, int top)
        {
            return MSDatabase.Query<Dto.LeagueMatch>("SELECT top(@Top) LeagueId, LeagueName, " +
                            " PlayRound, Season, LeagueMatchId, " +
                            " MatchResult, ResultText, TeamName1, TeamName2 " +
                            " from ms.vLeagueMatch where MatchResult > 0 and  Season=@Season" +
                            " order by LastUpdate desc",
                    new { Season = season, Top = top });
        }
        #endregion

        #region Tours
        public IEnumerable<Dto.Tour> GetSeasonTours(int season, DateTime afterDate)
        {
            MSDatabase.EnableAutoSelect = false;
            var list = MSDatabase.Query<Dto.Tour>("SELECT [TourId],[TourDate],[Description],LastRegistrationDate," +
                            "[OpenForSignUp],[MaxNoOfMembers],[NoOfMembers],[UrlDescription] " +
                            "FROM [ms].[Tour] " +
                            "WHERE datepart(year, [TourDate]) = @Season" +
                            "    and TourDate >= @AfterDate " +
                            "ORDER BY TourDate",
                    new { Season = season, AfterDate = afterDate });
            return list;
        }

        public IEnumerable<Dto.TourPlayer> GetTourPlayers(int tourId)
        {
            return MSDatabase.Fetch<Dto.TourPlayer>(";EXEC [ms].[TourPlayerSelect] @TourId",
                    new { TourId = tourId });
        }

        public Dto.Tour GetTour(int tourId)
        {
            return MSDatabase.Query<Dto.Tour>("SELECT [TourId],[TourDate],t.[Description],LastRegistrationDate," +
                            "[OpenForSignUp],[MaxNoOfMembers],[NoOfMembers],[UrlDescription],t.[SponsorLogoId], m.[Sponsor] " +
                            "FROM [ms].[Tour] as t left join ms.Match as m on t.MatchId = m.MatchId " +
                            "WHERE TourId = @TourId ",
                new { TourId = tourId }).FirstOrDefault();
        }


        public bool TourPlayerIsRegistered(int tourId, int vgcNo)
        {
            int x = MSDatabase.Fetch<Int32>("SELECT SignedUp " +
                            "FROM [ms].[TourPlayer] " +
                            "WHERE TourId = @TourId and VgcNo = @VgcNo ",
                new { TourId = tourId, VgcNo = vgcNo }).FirstOrDefault();

            return (x != 0);
        }

        public void TourChangeRegistration(int tourId, int vgcNo, string updateBy)
        {
            MSDatabase.Execute(";exec [ms].[TourChangeRegistration] @TourId, @VgcNo, @LastUpdateBy",
                    new { TourId = tourId, VgcNo = vgcNo, LastUpdateBy = updateBy});
        }

        public Data.Dto.Tour UpdateTour(Data.Dto.Tour m)
        {
            MSDatabase.EnableAutoSelect = false;
            SqlParameter tourId = new SqlParameter("TourId", System.Data.SqlDbType.Int);
            tourId.Direction = System.Data.ParameterDirection.InputOutput;
            tourId.Value = m.Id;

            var Tour = MSDatabase.Execute(";exec [ms].[TourUpsert] @TourId output, " +
                "@TourDate,@Description,@LastRegistrationDate,@OpenForSignUp,@MaxNoOfMembers,@UrlDescription," +
                "@NoOfMembers,@MatchId,@SponsorLogoId"
                    , new
                    {
                        TourId = tourId,
                        TourDate = m.TourDate,
                        Description = m.Description,
                        LastRegistrationDate = m.LastRegistrationDate,
                        OpenForSignUp = m.OpenForSignUp,
                        MaxNoOfMembers = m.MaxNoOfMembers,
                        UrlDescription = m.UrlDescription,
                        NoOfMembers = m.NoOfMembers,
                        MatchId = m.MatchId,
                        SponsorLogoId = m.SponsorLogoId
                    });
            m.Id = (int)tourId.Value;
            return m;
        }


        #endregion

        #region Course administration
        public Dto.Club GetClub(int clubId)
        {
            return MSDatabase.Fetch<Dto.Club>("SELECT ClubId, ClubName " 
                + "from ms.Club where ClubId = @ClubId",
                new { ClubId = clubId })
                .FirstOrDefault();
        }
        public IEnumerable<Dto.Club> GetClubs()
        {
            return MSDatabase.Fetch<Dto.Club>("SELECT ClubId, ClubName FROM "
                + "ms.Club ORDER BY ClubName");
        }
        public IEnumerable<Dto.ListEntry> GetCourseList(int clubId)
        {
            return MSDatabase.Fetch<Dto.ListEntry>("SELECT [CourseName] as [Value], [CourseId] as [Key] " +
                    "FROM [ms].[Course] " +
                    "where ClubId = @ClubId " +
                    "order by [CourseName]",
                new { ClubId = clubId });
        }
        public IEnumerable<Dto.CourseInfo> GetClubCourses(int clubId)
        {
            return MSDatabase.Fetch<Dto.CourseInfo>("SELECT [CourseName]" +
                    ",[ClubId],[ClubName],[CourseId],[Slope],[CourseRating],[Par],[Tee]" +
                    ",[CourseTeeId],[CourseDetailId],[IsMale] " +
                    "FROM [ms].[vCourseInfo] " +
                    "where IsMale = 1 and ClubId = @ClubId " +
                    "order by [CourseName], [Tee]",
                new { ClubId = clubId });
        }
        public IEnumerable<Dto.CourseInfo> GetCourses()
        {
            return MSDatabase.Fetch<Dto.CourseInfo>("SELECT [CourseName]" +
                    ",[ClubId],[ClubName],[CourseId],[Slope],[CourseRating],[Par],[Tee]" +
                    ",[CourseTeeId],[CourseDetailId],[IsMale] " +
                    "FROM [ms].[vCourseInfo] " +
                    "where IsMale = 1 " +
                    "order by [ClubName],[CourseName], [Tee]");
        }
        public Dto.CourseInfo GetCourse(int id)
        {
            return MSDatabase.Query<Dto.CourseInfo>("SELECT [CourseName]" +
                    ",[ClubId],[ClubName],[CourseId],[Slope],[CourseRating],[Par],[Tee]" +
                    ",[CourseTeeId],[CourseDetailId],[IsMale] " +
                    "FROM [ms].[vCourseInfo] " +
                    "where CourseDetailId = @CourseDetailId",
            new { CourseDetailId = id }).FirstOrDefault();
        }

        public IEnumerable<Dto.ListEntry> GetTees()
        {
            return MSDatabase.Query<Dto.ListEntry>("SELECT  [CourseTeeId] as [Key] " +
                    ",[Tee] as [Value] " +
                    "FROM [ms].[CourseTee]").ToList();
        }

        public void UpdateClub(Data.Dto.ListEntry m)
        {
            MSDatabase.EnableAutoSelect = false;

            if (m.Key > 0)
            {
                MSDatabase.Execute("UPDATE [ms].[Club] set [ClubName] = @ClubName " +
                    "WHERE ClubId = @Id"
                    , new { @ClubName = m.Value, @Id = m.Key });
            }
            else {
                MSDatabase.Execute("INSERT INTO [ms].[Club] ([ClubName]) " +
                    "VALUES (@ClubName)"
                        , new { @ClubName = m.Value });
            }
        }

        public Data.Dto.ListEntry UpdateCourse(Data.Dto.ListEntry m)
        {
            MSDatabase.EnableAutoSelect = false;
            SqlParameter courseId = new SqlParameter("CourseId", System.Data.SqlDbType.Int);
            courseId.Direction = System.Data.ParameterDirection.InputOutput;
            courseId.Value = -1;

            var match = MSDatabase.Execute(";exec [ms].[CourseUpsert] @CourseId output,"
                + " @ClubId,@CourseName  "
                    , new
                    {
                        @ClubId = m.Key
                        ,
                        @CourseId = courseId
                        ,
                        @CourseName = m.Value
                    });
            m.Key = (int)courseId.Value;
            return m;
        }

        public Data.Dto.CourseInfo UpdateCourseDetail(Data.Dto.CourseInfo m)
        {
            MSDatabase.EnableAutoSelect = false;
            SqlParameter id = new SqlParameter("CourseDetailId", System.Data.SqlDbType.Int);
            id.Direction = System.Data.ParameterDirection.InputOutput;
            id.Value = m.CourseDetailId;

            var match = MSDatabase.Execute(";exec [ms].[CourseDetailUpsert] @CourseDetailId out,"
                + " @CourseId,@CourseTeeId,@Par,@CourseRating,@Slope  "
                    , new {
                        @CourseDetailId = id
                        , @CourseId = m.CourseId
                        , @CourseTeeId = m.CourseTeeId
                        , @Par = m.Par
                        , @CourseRating = m.CourseRating
                        , @Slope = m.Slope
                    });
            m.CourseDetailId = (int)id.Value;
            return m;
        }

        #endregion

        #region Database
        private Database localDb;
        protected Database MSDatabase
        {
            get
            {
                if (null == localDb)
                    localDb = new Database("vgcmsSqlServer");
                return localDb;
            }
        }
        #endregion

        #region Settings

        public Dto.Property GetSetting(int id)
        {
            var query = MSDatabase.Fetch<Dto.Property>(";SELECT [PropertyId],[DataValue],[SystemType] FROM [ms].[Property] " +
                    "where PropertyId = PropertyId",
                new { PropertyId = id }).FirstOrDefault();
            return query;
        }
        public List<Dto.Property> GetSettings(DateTime dt)
        {
            string sql = ";with cte as ( " +
                "select max(validfrom) as dt, PropertyId " +
                "FROM[ms].[Property] " +
                "        where ValidFrom <= @Valid " +
                "        group by PropertyId " +
                ") " +
                "SELECT p.[PropertyId], [DataValue], [SystemType], ValidFrom " +
                "FROM [ms].[Property] as p inner join cte on " +
                "p.[PropertyId] = cte.[PropertyId] AND " +
                "p.ValidFrom = cte.dt";

            //var query = MSDatabase.Fetch<Dto.Property>(";SELECT [PropertyId],[DataValue],[SystemType],ValidFrom, ValidTo FROM [ms].[Property] " +
            //        "where (ValidFrom <= @Valid and (@Valid < ValidTo  or ValidTo is null)) Order by ValidFrom",
            var query = MSDatabase.Fetch<Dto.Property>(sql,
                new { Valid = dt });
            return query;
        }

        protected enum PropertyKey
        {
            Season,
            DSeasonStart,
            DSeasonEnd,
            ShowNumberOfEvents,
            ReadMoreEventUrl,
            ReadMoreResultUrl,
            MemberAdminUrl,
            MensSectionLogoUrl,
            MensSection,
            MinRoundsPlayed,
            MensSectionSponsor,
            GroupAUpperBound,
            GroupBUpperBound,
            WsAccount,
            WsUsername,
            WsPassword,
            WsGroupGuid
        }

        public string WsAccount
        {
            get { return GetPropertyValue<string>(PropertyKey.WsAccount); }
        }

        public string WsUsername
        {
            get { return GetPropertyValue<string>(PropertyKey.WsUsername); }
        }

        public string WsPassword
        {
            get { return GetPropertyValue<string>(PropertyKey.WsPassword); }
        }

        public string WsGroupGuid
        {
            get { return GetPropertyValue<string>(PropertyKey.WsGroupGuid); }
        }

        public DateTime SeasonStart
        {
            get { return new DateTime(GetPropertyValue<int>(PropertyKey.Season), 1, 1); }
        }

        public int Season
        {
            get { return GetPropertyValue<int>(PropertyKey.Season); }
        }

        public DateTime SeasonEnd
        {
            get { return new DateTime(GetPropertyValue<int>(PropertyKey.Season), 12, 31); }
        }

        private List<KeyValuePair<int, string>> settings;
        protected TValue GetPropertyValue<TValue>(PropertyKey key)
        {
            if (settings == null)
            {
                settings = GetSettings(DateTime.Now).Select(x => 
                        new KeyValuePair<int, string>(x.Id, x.Value)).ToList();
            }

            var val = settings.Where(x => x.Key == (int)key).FirstOrDefault().Value;
            if (null == val)
                return default(TValue);

            switch (key)
            {
                case PropertyKey.MensSectionLogoUrl:
                case PropertyKey.MensSection:
                case PropertyKey.WsAccount:
                case PropertyKey.WsUsername:
                case PropertyKey.WsPassword:
                case PropertyKey.WsGroupGuid:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
                case PropertyKey.DSeasonStart:
                case PropertyKey.DSeasonEnd:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
                //case PropertyKey.MensSectionLogoUrl:
                case PropertyKey.Season:
                case PropertyKey.MinRoundsPlayed:
                case PropertyKey.MensSectionSponsor:
                case PropertyKey.GroupAUpperBound:
                case PropertyKey.GroupBUpperBound:
                    return (TValue)Convert.ChangeType(val, typeof(TValue));
            }
            return default(TValue);
        }
        #endregion
    }

}
