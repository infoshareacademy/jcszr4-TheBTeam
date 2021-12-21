using AutoMapper;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<User, UserDto>();
            CreateMap<Test, TestDto>();
        } 
    }
}