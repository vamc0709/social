using Social.DTOs;

namespace Social.Models;



public record Post
{


    public long PostId { get; set; }
    public DateTimeOffset PostDate { get; set; }
    public long UserId { get; set; }
    public long NoOfImages { get; set; }
    
     public PostDTO asDto => new PostDTO
     {
    
        PostId = PostId,
        PostDate = PostDate,
        UserId = UserId,
        NoOfImages = NoOfImages,
        
    };

    
}