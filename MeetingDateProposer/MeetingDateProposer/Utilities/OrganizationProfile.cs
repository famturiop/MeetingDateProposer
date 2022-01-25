using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeetingDateProposer.Domain.Models.ApplicationModels;
using MeetingDateProposer.Models.ApplicationApiModels;

namespace MeetingDateProposer.Utilities
{
    public class OrganizationProfile: Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Meeting, MeetingApiModel>();
            CreateMap<Calendar, CalendarApiModel>();
            CreateMap<CalendarEvent, CalendarEventApiModel>()
                .ForMember(dest => dest.Start, opt => opt.MapFrom(src => src.EventStart))
                .ForMember(dest => dest.End, opt => opt.MapFrom(src => src.EventEnd));
            CreateMap<ApplicationUser, ApplicationUserApiModel>();

            CreateMap<MeetingApiModel, Meeting>();
            CreateMap<CalendarApiModel, Calendar>();
            CreateMap<CalendarEventApiModel, CalendarEvent>()
                .ForMember(dest => dest.EventStart, opt => opt.MapFrom(src => src.Start))
                .ForMember(dest => dest.EventEnd, opt => opt.MapFrom(src => src.End));
            CreateMap<ApplicationUserApiModel, ApplicationUser>();
        }
    }
}
