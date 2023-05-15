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
            CreateMap<Loan,LoanResponseDTO>()
                .ForMember(dto=>dto.CustomerName,ent=>ent.MapFrom(x=>$"{x.Customer.FirstName} {x.Customer.LastName}"))
                .ForMember(dto=>dto.CustomerId,ent=>ent.MapFrom(x=>x.Customer.Id))
                .ForMember(dto=>dto.LoanStatus,ent=>ent.MapFrom(x=>x.LoanStatus.Name))
                .ForMember(dto=>dto.LoanType,ent=>ent.MapFrom(x=>x.LoanType.Name))
                ;
            CreateMap<Account, AccountResponseDTO>()
                .ForMember(dto => dto.Name, ent => ent.MapFrom(x => x.AccountName))
                .ForMember(dto=>dto.Propetary,ent=>ent.MapFrom(x=>$"{x.Customer.FirstName} {x.Customer.LastName}"))
                .ForMember(dto=>dto.Status,ent=>ent.MapFrom(x=>x.AccountStatus.Description))
                .ForMember(dto=>dto.PropetaryId,ent=>ent.MapFrom(x=>x.Customer.Id))
                .ForMember(dto=>dto.Type,ent=>ent.MapFrom(x=>x.AccountType.Name));
        }
    }
}
