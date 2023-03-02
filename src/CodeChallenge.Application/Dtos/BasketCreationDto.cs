namespace CodeChallenge.Application.Dtos;
public sealed class BasketCreationDto
{
    public string? UserEmail { get; set; }
    public ICollection<BasketItemCreationDto>? Items { get; set; }
}
