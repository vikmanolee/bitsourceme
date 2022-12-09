using AutoMapper;
using BitSourceMe.Core.Abstractions.Models;
using TickerPriceViewModel = BitSourceMe.WebApi.ViewModels.TickerPrice;

namespace BitSourceMe.WebApi.Mappers;

public class TickerPriceProfile : Profile
{
    public TickerPriceProfile()
    {
        CreateMap<TickerPrice, TickerPriceViewModel>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(
                src => decimal.Round(src.Price, 2, MidpointRounding.AwayFromZero).ToString("F")))
            .ForMember(dest => dest.SourceCode, opt => opt.MapFrom(src => src.SourceCode))
            .ForMember(dest => dest.FetchedDate, opt => opt.MapFrom(src => src.FetchedDate));
    }
}
