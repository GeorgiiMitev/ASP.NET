namespace SocialMediaPlatform.Dtos
{
    public record CommentReadDto(int Id, string Content, DateTime CreatedAt);
    public record CommentCreateDto(string Content, int PostId);
}
