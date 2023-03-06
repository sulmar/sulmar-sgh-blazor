namespace SGH.Shopper.Domain;

public class PagingParameters
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}

public class RequestParameters
{
    public int StartIndex { get; set; }
    public int Count { get; set; }
}


public class PagingResponse<TITem>
{
    public IEnumerable<TITem> Items { get; set; }
    public int TotalCount { get; set; }
}