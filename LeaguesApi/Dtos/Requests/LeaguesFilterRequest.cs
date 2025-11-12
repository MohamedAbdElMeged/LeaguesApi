namespace LeaguesApi.Dtos.Requests;

public class LeaguesFilterRequest
{
    public string? Name { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}