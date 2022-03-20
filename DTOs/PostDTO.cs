using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Social.DTOs;
public record PostDTO
{
    [JsonPropertyName("post_id")]
    public long PostId { get; set; }

    [JsonPropertyName("post_date")]
    public DateTimeOffset PostDate { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

    [JsonPropertyName("no_of_images")]
    public long NoOfImages { get; set; }

    public List<LikesDTO> Likes{get; internal set;}

}

public record PostCreateDTO
{


    [JsonPropertyName("post_id")]

    public long PostId { get; set; }

    [JsonPropertyName("post_date")]

    public DateTimeOffset PostDate { get; set; }

    [JsonPropertyName("no_of_images")]
    [Required]
    public long NoOfImages { get; set; }

    [JsonPropertyName("user_id")]
    public long UserId { get; set; }

}


