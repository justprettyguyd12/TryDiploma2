using AutoMapper;
using DataAccess.Models;
using DataAccess.Models.Crm;
using Domain.Models;
using Domain.Models.Crm;
using TryDiploma.ViewModel;
using TryDiploma.ViewModel.DealModels;
using TryDiploma.ViewModel.FunnelModels;
using TryDiploma.ViewModel.SectionModels;

namespace TryDiploma;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddFunnelModel, Funnel>();
        CreateMap<AddSectionModel, Section>();
        CreateMap<UpdateSectionModel, Section>();
        CreateMap<AddBeatModel, Beat>().ReverseMap();
        // CreateMap<ClientEntity, Client>().ReverseMap();
        // CreateMap<ContractEntity, Contract>().ReverseMap();
        CreateMap<AddDealModel, Deal>();
        // CreateMap<ShoppingBagEntity, ShoppingBag>().ReverseMap();
    }
}