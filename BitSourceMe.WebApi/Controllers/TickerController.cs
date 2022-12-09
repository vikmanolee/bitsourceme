using AutoMapper;
using BitSourceMe.Core.Abstractions;
using BitSourceMe.Core.Abstractions.Models;
using Microsoft.AspNetCore.Mvc;
using SearchCriteria = BitSourceMe.WebApi.RequestModels.SearchCriteria;
using TickerPrice = BitSourceMe.WebApi.ViewModels.TickerPrice;
using TickerSource = BitSourceMe.WebApi.ViewModels.TickerSource;

namespace BitSourceMe.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TickerController : ControllerBase
{
    private readonly ILogger<TickerController> _logger;
    private readonly ISourceService _sourceService;
    private readonly IPriceService _priceService;
    private readonly IMapper _mapper;

    public TickerController(ILogger<TickerController> logger,
        IMapper mapper,
        ISourceService sourceService,
        IPriceService priceService
        )
    {
        _logger = logger;
        _sourceService = sourceService;
        _priceService = priceService;
        _mapper = mapper;
    }
    
    [HttpGet("Sources")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<TickerSource>> GetSources()
    {
        var sources = await _sourceService.GetAllSources();
        return _mapper.Map<IEnumerable<TickerSource>>(sources);
    }

    [HttpGet("Sources/{sourceCode}/Fetch")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TickerPrice>> FetchFromSource(string sourceCode)
    {
        try
        {
            var newPrice = await _priceService.FetchNewPrice(sourceCode);
            return _mapper.Map<TickerPrice>(newPrice);
        }
        catch (ArgumentException aex)
        {
            _logger.LogWarning(aex, "Source code `{0}` requested not found", sourceCode);
            return NotFound(sourceCode);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error in request to source `{0}`", sourceCode);
            return Problem("The HTTP request to the the ticker source could not be fulfilled");
        }
    }

    [HttpPost("Prices/Search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IEnumerable<TickerPrice>> SearchTickerPricesHistory(SearchCriteria criteria)
    {
        var searchCriteria = new Core.Abstractions.Models.SearchCriteria
        {
            SourceCode = criteria.SourceCode,
            DateRange = new DateRange
            {
                DateFrom = criteria.DateFrom,
                DateTo = criteria.DateTo
            }
        };
        var prices = await _priceService.GetTickerPrices(searchCriteria);
        return _mapper.Map<IEnumerable<TickerPrice>>(prices);
    }
}
