using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.Crm;
using Domain.Models;
using Domain.Models.Crm;

namespace DataAccess;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<FunnelEntity, Funnel>().ReverseMap();
        CreateMap<SectionEntity, Section>().ReverseMap();
        CreateMap<BeatEntity, Beat>().ReverseMap();
        CreateMap<ClientEntity, Client>().ReverseMap();
        CreateMap<ContractEntity, Contract>().ReverseMap();
        CreateMap<DealEntity, Deal>().ReverseMap();
        CreateMap<ShoppingBagEntity, ShoppingBag>().ReverseMap();
    }
}