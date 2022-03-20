


using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Social.DTOs;

public record HashtagDTO
{
    [JsonPropertyName("hash_id")]
    public long HashId { get; set; }

     [JsonPropertyName("hash_name")]
    public string HashName { get; set; }

    
    
    public List<PostDTO> Post { get; internal set; }
}

public record HashtagCreateDTO
{
    [JsonPropertyName("hash_id")]
    [Required]
    public int HashId { get; set; }

     [JsonPropertyName("hash_name")]
     [Required]
     [MaxLength(50)]

     public string HashName { get; set; }

    
}

public record HashtagUpdateDTO
{


     [JsonPropertyName("hash_name")]
     [MaxLength(50)]
    public string Name { get; set; }
}
