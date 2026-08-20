namespace Application.Shared;

public class PaginationParams
{
    private int _pageNumber = 1;
    private int _pageSize = 20;

    private const int PageMaxSize = 50;
    
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > PageMaxSize || value < 0 ? PageMaxSize : value;
    }
}