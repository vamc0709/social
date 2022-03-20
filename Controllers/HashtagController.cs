


using Microsoft.AspNetCore.Mvc;
using social.Models;
using Social.DTOs;
using Social.Repositories;

namespace Social.Controllers;


[ApiController]
[Route("api/hashtags")]


public class HashtagController : ControllerBase 
{

    private readonly ILogger<HashtagController> _logger;
    private readonly IHashtagRepository _hashtag;
    private readonly IPostRepository _post;

    public HashtagController(ILogger<HashtagController> logger,
    IHashtagRepository hashtags,IPostRepository post)
    {
        _logger = logger;
        _hashtag = hashtags;
        _post = post;
    }
    
    
    [HttpGet]

    public async Task<ActionResult<List<LikesDTO>>> GetAllUser()
    {
        var usersList = await _hashtag.GetList();
        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }


    [HttpGet("{hash_id}")]
    public async Task<ActionResult<HashtagDTO>> GetHashtagById([FromRoute] int hash_id)
    {
        var hashtag = await _hashtag.GetById(hash_id);

        if (hashtag is null)
            return NotFound("No hashtag found with given id");

        var dto = hashtag.asDto;
        dto.Post = await _post.GetAllForHashtag(hashtag.HashId);
        return Ok(dto);
    }

   [HttpPost]
    public async Task<ActionResult<HashtagDTO>> CreateUser([FromBody] HashtagCreateDTO Data)
    {

              var post = await _post.GetById(Data.HashId);
        if (post is null)
            return NotFound("No hastag found with given post id");


        var toCreateHashtag = new Hashtag
        {
            HashName = Data.HashName,
            HashId = Data.HashId,
           
        };

        var createdHashtag = await _hashtag.Create(toCreateHashtag);

        return StatusCode(StatusCodes.Status201Created, createdHashtag.asDto);
    }


    
    [HttpPut("{hash_id}")]
    public async Task<ActionResult> UpdateHashtag([FromRoute] long id,
    [FromBody] HashtagUpdateDTO Data)
    {
        var existing = await _hashtag.GetById(id);
        if (existing is null)
            return NotFound("No hastag found with given id");

        var toUpdateHashtag = existing with
        {
          
          HashName = Data.Name?.Trim() ?? existing.HashName,  
        };

        var didUpdate = await _hashtag.Update(toUpdateHashtag);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update hastag");

        return NoContent();
    }


     [HttpDelete("{hash_id}")]
    public async Task<ActionResult> DeleteHashtag([FromRoute] long id)
    {
        var existing = await _hashtag.GetById(id);
        if (existing is null)
            return NotFound("No hashtag found with given id");

        var didDelete = await _hashtag.Delete(id);

        return NoContent();
    }
}