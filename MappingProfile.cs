using AutoMapper;
using WinemakerAPI.Entities;
using WinemakerAPI.Models;

namespace WinemakerAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // winemakers mappings
            CreateMap<PostWineMaker, WineMaker>();
            CreateMap<WineMaker, GetWineMaker>();
        }
    }
}
