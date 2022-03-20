

using Social.DTOs;


namespace Social.Models;

public record Likes
{
    public long LikeId { get; set; }
    public DateTimeOffset CreatedAt { get;set;}
    public long PostId{ get; set; }
    public long UserId { get;set; }


    public LikesDTO asDto => new LikesDTO
    {
        LikeId = LikeId,
        CreatedAt = CreatedAt,
        PostId = PostId,
        UserId = UserId,
    };


    
}
