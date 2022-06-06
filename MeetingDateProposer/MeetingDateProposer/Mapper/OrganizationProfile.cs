using AutoMapper;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Models.ApplicationApiModels;
using System;

namespace MeetingDateProposer.Mapper
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Meeting, MeetingApiModel>();
            CreateMap<Calendar, CalendarApiModel>();
            CreateMap<CalendarEvent, CalendarEventApiModel>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.EventStart))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EventEnd));
            CreateMap<ApplicationUser, ApplicationUserApiModel>();

            CreateMap<MeetingApiModel, Meeting>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == "" ? Guid.Empty : Guid.Parse(src.Id)));
            CreateMap<CalendarApiModel, Calendar>();
            CreateMap<CalendarEventApiModel, CalendarEvent>()
                .ForMember(dest => dest.EventStart, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.EventEnd, opt => opt.MapFrom(src => src.End));
            CreateMap<ApplicationUserApiModel, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id == "" ? Guid.Empty : Guid.Parse(src.Id)));
        }
    }
}
