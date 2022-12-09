using AutoMapper;
using BitSourceMe.Core.Abstractions.Models;
using TickerSourceViewModel = BitSourceMe.WebApi.ViewModels.TickerSource;

namespace BitSourceMe.WebApi.Mappers;

public class TickerSourceProfile : Profile
{
    public TickerSourceProfile()
    {
        CreateMap<TickerSource, TickerSourceViewModel>();
    }
}
