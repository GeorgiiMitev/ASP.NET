namespace SocialMediaPlatform.Dtos
{
    //In short: A record is actually a special kind of class that the compiler enhances with extra "boiler-plate" code.
    public record BlogReadDto(int Id, string Name, int UserId);
    public record BlogReadDtoList(int Id, string Name, UserReadDto UserDto,List<PostReadDto> PostDto);
    public record BlogCreateDto(string Name, int UserId);
}
