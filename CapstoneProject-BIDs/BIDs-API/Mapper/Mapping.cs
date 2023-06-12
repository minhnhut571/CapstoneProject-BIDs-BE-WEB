using AutoMapper;
using Business_Logic.Modules.StaffModule.Response;
using Business_Logic.Modules.UserModule.Response;
using Data_Access.Entities;

namespace BIDs_API.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Staff, StaffResponse>()
                .ForMember(x => x.AccountName, d => d.MapFrom(s => s.AccountName))
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email))
                .ForMember(x => x.StaffName, d => d.MapFrom(s => s.StaffName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone));
            CreateMap<User, UserResponse>()
                .ForMember(x => x.AccountName, d => d.MapFrom(s => s.AccountName))
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.UserName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone))
                .ForMember(x => x.CCCDNumber, d => d.MapFrom(s => s.Cccdnumber))
                .ForMember(x => x.CCCDBackImage, d => d.MapFrom(s => s.CccdbackImage))
                .ForMember(x => x.CCCDFrontImage, d => d.MapFrom(s => s.CccdfrontImage));
        }
    }
}
