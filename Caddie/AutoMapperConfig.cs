using Caddie.Models;
using System.Globalization;

namespace Caddie
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Data.Dto.ResultMatch, ResultRegistrationModel>()
                            .ForMember(dest => dest.Birdies,
                                        opts => opts.MapFrom(src => src.Birdies))
                        .ReverseMap()
                            .ForMember(dest => dest.Hcp,
                                        opts => opts.MapFrom(src => src.Hcp))
                            .ForMember(dest => dest.Hcp,
                                        opts => opts.MapFrom(src => src.MatchformId == 1 ? src.Brutto - src.Netto : src.Hcp));

            AutoMapper.Mapper.CreateMap<Data.Dto.ListDateTimeEntry, TimeItemModel>()
                            .ForMember(dest => dest.DateTimeValue,
                                        opts => opts.MapFrom(src => src.Value))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Key));

            AutoMapper.Mapper.CreateMap<Data.Dto.ListDateTimeEntry, ItemModel>()
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => src.Value.ToString("dd. MMM, yyyy")))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Key));

            AutoMapper.Mapper.CreateMap<Data.Dto.ListEntry, ItemModel>()
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => src.Value))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Key))
                        .ReverseMap()
                            .ForMember(dest => dest.Value,
                                        opts => opts.MapFrom(src => src.StringValue))
                            .ForMember(dest => dest.Key,
                                        opts => opts.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Data.Dto.NearestPinResult, NearestPinResultModel>()
                                        .ReverseMap();

            AutoMapper.Mapper.CreateMap<Data.Dto.Club, ItemModel>()
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => src.Name))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Data.Dto.Tour, ItemModel>()
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => src.Description))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Data.Dto.CompetitionResult, CompetitionResultModel>()
                            .ReverseMap();

            #region Match
            AutoMapper.Mapper.CreateMap<Data.Dto.Match, ItemModel>()
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => src.MatchDate.ToString("dd. MMM, yyyy")))
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.Id));
            AutoMapper.Mapper.CreateMap<Caddie.Data.Dto.Match, MatchModel>()
                            .ForMember(dest => dest.MonthOfEvent,
                                        opts => opts.MapFrom(src => src.MatchDate.ToString("MMM", CultureInfo.CreateSpecificCulture("da"))))
                            .ForMember(dest => dest.Tee,
                                        opts => opts.MapFrom(src => src.Tee.Trim()))
                                        .ReverseMap();
            #endregion

            #region Course
            AutoMapper.Mapper.CreateMap<Data.Dto.CourseInfo, CourseModel>()
                    .ForMember(dest => dest.Id,
                                opts => opts.MapFrom(src => src.CourseDetailId))
                    .ReverseMap()
                    .ForMember(dest => dest.CourseDetailId,
                                opts => opts.MapFrom(src => src.Id));
            AutoMapper.Mapper.CreateMap<Data.Dto.CourseInfo, ItemModel>()
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.CourseDetailId))
                            .ForMember(dest => dest.StringValue,
                                        opts => opts.MapFrom(src => string.Equals(src.ClubName, src.CourseName, System.StringComparison.InvariantCultureIgnoreCase)
                                            ? string.Format("{0} - Tee: {1}", src.ClubName, src.Tee)
                                            : string.Format("{0}, {1} - Tee: {2}", src.ClubName, src.CourseName, src.Tee)
                                            ));

            #endregion
            
            #region Tour

            AutoMapper.Mapper.CreateMap<Data.Dto.Tour, TourModel>()
                    .ReverseMap();

            #endregion
            #region Player

            AutoMapper.Mapper.CreateMap<Caddie.Data.Dto.Player, PlayerModel>()
                    .ForMember(dest => dest.Name,
                                opts => opts.MapFrom(src => string.Format("{0}, {1}", src.Lastname, src.Firstname)))
                    .ForMember(dest => dest.NameGroup,
                                opts => opts.MapFrom(src => src.NameGroup))
                    .ReverseMap();

            AutoMapper.Mapper.CreateMap<Caddie.Data.Dto.Player, ItemModel>()
                     .ForMember(dest => dest.StringValue,
                                 opts => opts.MapFrom(src => string.Format("{0}, {1}", src.Lastname, src.Firstname)))
                     .ForMember(dest => dest.Id,
                                 opts => opts.MapFrom(src => src.MemberShipId));

            AutoMapper.Mapper.CreateMap<Caddie.Data.Dto.Membership, PlayerModel>();

            #endregion
            #region Matchplay

            AutoMapper.Mapper.CreateMap<Data.Dto.LeagueMatch, MatchplayMatchModel>()
                            .ForMember(dest => dest.Id,
                                        opts => opts.MapFrom(src => src.LeagueMatchId))
                    .ReverseMap()
                            .ForMember(dest => dest.LeagueMatchId,
                                        opts => opts.MapFrom(src => src.Id));

            AutoMapper.Mapper.CreateMap<Data.Dto.LeagueTeam, MatchplayTeamModel>()
                    .ReverseMap();
            #endregion
        }
    }
}
