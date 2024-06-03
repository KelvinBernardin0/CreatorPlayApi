namespace CreatorPlay.Application.Common.Models.Response;

public class PaginatedModelResponse<T>
{
    public int Total { get; set; }
    public IEnumerable<T> Items { get; set; }

    protected PaginatedModelResponse() { }

    public PaginatedModelResponse(int total, IEnumerable<T> items)
    {
        this.Total = total;
        this.Items = items;
    }
}
