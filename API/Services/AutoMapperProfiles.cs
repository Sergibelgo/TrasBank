using APITrassBank.Models;
using AutoMapper;
using Entitys.Entity;
namespace APITrassBank.Services
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CustomerEditDTO, Customer>();
        }
    }
}
