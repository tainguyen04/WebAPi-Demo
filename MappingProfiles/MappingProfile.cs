using AutoMapper;
using DemoDangTin.DTO.Request;
using DemoDangTin.DTO.Response;
using DemoDangTin.Entities;

namespace DemoDangTin.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBaiDangRequest,BaiDang>()
                .ForMember(dest => dest.Id,opt => opt.Ignore())
                .ForMember(dest => dest.NgayDang,opt => opt.Ignore());
            CreateMap<BaiDang, BaiDangResponse>();
            CreateMap<UpdateBaiDangRequest,BaiDang>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<AuthenticationRequest, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, AuthenticationResponse>();
        }
    }
}
