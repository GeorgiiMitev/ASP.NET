namespace SocialMediaPlatform.Dtos
{
    public record UserReadDto(int Id, string Name, string Password);

    public record UserCreateDto(string Name, string Password);

}
