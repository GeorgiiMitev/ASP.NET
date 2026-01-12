namespace SocialMediaPlatform.Dtos
{
    public record BlogReadDto(int Id, string Name, int UserId);

    public record BlogCreateDto(string Name, int UserId);

    public record BlogReadListDto(int Id, string Name, UserReadDto UserDto);

}

