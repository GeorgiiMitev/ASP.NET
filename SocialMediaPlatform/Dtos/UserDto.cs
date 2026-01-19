namespace SocialMediaPlatform.Dtos
{
    public record UserReadDto(int Id, string Username, string? BlogName);
    public record UserCreateDto(string Username, string Password);
}
