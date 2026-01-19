namespace SocialMediaPlatform.Dtos
{
    public record PostReadDto(int Id, string Title, string Content, List<TagReadDto> Tags);
    public record PostCreateDto(string Title, string Content, int BlogId, List<int> TagIds);
}
