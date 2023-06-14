using AutoMapper;
using Business_Logic.Modules.BanHistoryModule.Response;
using Business_Logic.Modules.ItemModule.Response;
using Business_Logic.Modules.ItemTypeModule.Response;
using Business_Logic.Modules.SessionDetailModule.Response;
using Business_Logic.Modules.SessionModule.Response;
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
                .ForMember(x => x.Password, d => d.MapFrom(s => s.Password))
                .ForMember(x => x.StaffName, d => d.MapFrom(s => s.StaffName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone));
            CreateMap<Staff, StaffResponseAdmin>()
                .ForMember(x => x.StaffId, d => d.MapFrom(s => s.StaffId))
                .ForMember(x => x.RoleName, d => d.MapFrom(s => s.Role.RoleName))
                .ForMember(x => x.AccountName, d => d.MapFrom(s => s.AccountName))
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email))
                .ForMember(x => x.StaffName, d => d.MapFrom(s => s.StaffName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));

            CreateMap<User, UserResponse>()
                .ForMember(x => x.AccountName, d => d.MapFrom(s => s.AccountName))
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email))
                .ForMember(x => x.Password, d => d.MapFrom(s => s.Password))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.UserName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone))
                .ForMember(x => x.CCCDNumber, d => d.MapFrom(s => s.Cccdnumber))
                .ForMember(x => x.CCCDBackImage, d => d.MapFrom(s => s.CccdbackImage))
                .ForMember(x => x.CCCDFrontImage, d => d.MapFrom(s => s.CccdfrontImage));
            
            CreateMap<User, UserResponseStaff>()
                .ForMember(x => x.UserId, d => d.MapFrom(s => s.UserId))
                .ForMember(x => x.AccountName, d => d.MapFrom(s => s.AccountName))
                .ForMember(x => x.Email, d => d.MapFrom(s => s.Email))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.UserName))
                .ForMember(x => x.DateOfBirth, d => d.MapFrom(s => s.DateOfBirth))
                .ForMember(x => x.Address, d => d.MapFrom(s => s.Address))
                .ForMember(x => x.Phone, d => d.MapFrom(s => s.Phone))
                .ForMember(x => x.Cccdnumber, d => d.MapFrom(s => s.Cccdnumber))
                .ForMember(x => x.CccdbackImage, d => d.MapFrom(s => s.CccdbackImage))
                .ForMember(x => x.CccdfrontImage, d => d.MapFrom(s => s.CccdfrontImage))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
            
            CreateMap<Session, SessionResponse>()
                .ForMember(x => x.ItemName, d => d.MapFrom(s => s.Item.ItemName))
                .ForMember(x => x.SessionName, d => d.MapFrom(s => s.SessionName))
                .ForMember(x => x.BeginTime, d => d.MapFrom(s => s.BeginTime))
                .ForMember(x => x.AuctionTime, d => d.MapFrom(s => s.AuctionTime))
                .ForMember(x => x.EndTime, d => d.MapFrom(s => s.EndTime))
                .ForMember(x => x.FinailPrice, d => d.MapFrom(s => s.FinailPrice));
            
            CreateMap<Session, SessionResponseStaff>()
                .ForMember(x => x.SessionId, d => d.MapFrom(s => s.SessionId))
                .ForMember(x => x.ItemName, d => d.MapFrom(s => s.Item.ItemName))
                .ForMember(x => x.SessionName, d => d.MapFrom(s => s.SessionName))
                .ForMember(x => x.BeginTime, d => d.MapFrom(s => s.BeginTime))
                .ForMember(x => x.AuctionTime, d => d.MapFrom(s => s.AuctionTime))
                .ForMember(x => x.EndTime, d => d.MapFrom(s => s.EndTime))
                .ForMember(x => x.FinailPrice, d => d.MapFrom(s => s.FinailPrice))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
            
            CreateMap<SessionDetail, SessionDetailResponse>()
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.User.UserName))
                .ForMember(x => x.SessionName, d => d.MapFrom(s => s.Session.SessionName))
                .ForMember(x => x.Price, d => d.MapFrom(s => s.Price))
                .ForMember(x => x.CreateDate, d => d.MapFrom(s => s.CreateDate));
            
            CreateMap<SessionDetail, SessionDetailResponseStaff>()
                .ForMember(x => x.SessionDetailId, d => d.MapFrom(s => s.SessionDetailId))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.User.UserName))
                .ForMember(x => x.SessionName, d => d.MapFrom(s => s.Session.SessionName))
                .ForMember(x => x.Price, d => d.MapFrom(s => s.Price))
                .ForMember(x => x.CreateDate, d => d.MapFrom(s => s.CreateDate))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
            
            CreateMap<ItemType, ItemTypeResponseStaff>()
                .ForMember(x => x.ItemTypeId, d => d.MapFrom(s => s.ItemTypeId))
                .ForMember(x => x.ItemTypeName, d => d.MapFrom(s => s.ItemTypeName))
                .ForMember(x => x.CreateDate, d => d.MapFrom(s => s.CreateDate))
                .ForMember(x => x.UpdateDate, d => d.MapFrom(s => s.UpdateDate))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
            
            CreateMap<Item, ItemResponse>()
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.User.UserName))
                .ForMember(x => x.ItemName, d => d.MapFrom(s => s.ItemName))
                .ForMember(x => x.ItemTypeName, d => d.MapFrom(s => s.ItemType.ItemTypeName))
                .ForMember(x => x.Description, d => d.MapFrom(s => s.Description))
                .ForMember(x => x.Quantity, d => d.MapFrom(s => s.Quantity))
                .ForMember(x => x.Image, d => d.MapFrom(s => s.Image))
                .ForMember(x => x.FristPrice, d => d.MapFrom(s => s.FristPrice))
                .ForMember(x => x.StepPrice, d => d.MapFrom(s => s.StepPrice))
                .ForMember(x => x.Deposit, d => d.MapFrom(s => s.Deposit));
            
            CreateMap<Item, ItemResponseStaff>()
                .ForMember(x => x.ItemId, d => d.MapFrom(s => s.ItemId))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.User.UserName))
                .ForMember(x => x.ItemName, d => d.MapFrom(s => s.ItemName))
                .ForMember(x => x.ItemTypeName, d => d.MapFrom(s => s.ItemType.ItemTypeName))
                .ForMember(x => x.Description, d => d.MapFrom(s => s.Description))
                .ForMember(x => x.Quantity, d => d.MapFrom(s => s.Quantity))
                .ForMember(x => x.Image, d => d.MapFrom(s => s.Image))
                .ForMember(x => x.FristPrice, d => d.MapFrom(s => s.FristPrice))
                .ForMember(x => x.StepPrice, d => d.MapFrom(s => s.StepPrice))
                .ForMember(x => x.Deposit, d => d.MapFrom(s => s.Deposit))
                .ForMember(x => x.CreateDate, d => d.MapFrom(s => s.CreateDate))
                .ForMember(x => x.UpdateDate, d => d.MapFrom(s => s.UpdateDate))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
            
            CreateMap<BanHistory, BanHistoryResponseStaff>()
                .ForMember(x => x.BanId, d => d.MapFrom(s => s.BanId))
                .ForMember(x => x.UserName, d => d.MapFrom(s => s.User.UserName))
                .ForMember(x => x.Reason, d => d.MapFrom(s => s.Reason))
                .ForMember(x => x.CreateDate, d => d.MapFrom(s => s.CreateDate))
                .ForMember(x => x.UpdateDate, d => d.MapFrom(s => s.UpdateDate))
                .ForMember(x => x.Status, d => d.MapFrom(s => s.Status));
        }
    }
}
