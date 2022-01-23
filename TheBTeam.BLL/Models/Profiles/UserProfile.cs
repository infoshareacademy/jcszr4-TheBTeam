using AutoMapper;
using TheBTeam.BLL.DAL.Entities;

namespace TheBTeam.BLL.Models.Profiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>();
        }
    }
}