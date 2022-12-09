namespace BitSourceMe.WebApi.RequestModels;

public class SearchCriteria
{
    public string? SourceCode { get; set; }
    
    public DateTime DateFrom { get; set; }
    
    public DateTime? DateTo { get; set; }
}
