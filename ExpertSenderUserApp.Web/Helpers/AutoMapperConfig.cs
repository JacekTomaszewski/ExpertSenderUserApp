using AutoMapper;
using ExpertSenderUserApp.Models.DTOs;
using ExpertSenderUserApp.Models.Entities;

namespace ExpertSenderUserApp.Web.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
