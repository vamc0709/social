using Microsoft.AspNetCore.Mvc;
using Social.DTOs;
using Social.Models;
using Social.Repositories;

namespace Social.Controllers;


[ApiController]
[Route("api/likes")]
public class LikesController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _post;
    private readonly ILikesRepository _likes;

    public LikesController(ILogger<PostController> logger,
    ILikesRepository likes, IPostRepository post)
    {
        _logger = logger;
        _post = post;
        _likes = likes;
    }

    [HttpGet]

    public async Task<ActionResult<List<LikesDTO>>> GetAllUser()
    {
        var usersList = await _likes.GetList();

        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    // [HttpPut("{likes_id}")]
    // public async Task<ActionResult> UpdateLikes([FromRoute] long id,
    // [FromBody] LikesUpdateDTO Data)
    // {
    //     var existing = await _likes.GetById(id);
    //     if (existing is null)
    //         return NotFound("No hastag found with given id");

    //     var toUpdateLikestag = existing with
    //     {
          
    //       LikesName = Data.Name?.Trim() ?? existing.LikesName,

           
            
    //     };

    //     var didUpdate = await _likes.Update(toUpdateLikestag);

    //     if (!didUpdate)
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Could not update likes");

    //     return NoContent();
    // }

    
    // [HttpGet("{likes_id}")]
    // public async Task<ActionResult<LikesDTO>> GetLikesById([FromRoute] int likes_id)
    // {
    //     var likes = await _likes.GetById(likes_id);

    //     if (likes is null)
    //         return NotFound("No likes found with given id");

    //     var dto = likes.asDto;
    //     dto.Post = await _post.GetAllLikes(likes.LikeId);
    //     return Ok(dto);
    // }
    [HttpPost]
    public async Task<ActionResult<LikesCreateDTO>> CreatePost([FromBody] LikesCreateDTO Data)
    {
        var post = await _post.GetById(Data.PostId);
        if (post is null)
            return NotFound("No likes found with given user id");

        var toCreateLikes = new Likes
        {

            PostId = Data.PostId,
            UserId = Data.UserId,
            CreatedAt = Data.CreatedAt.UtcDateTime,
        };
        var createdLikes = await _likes.Create(toCreateLikes);

        return StatusCode(StatusCodes.Status201Created, createdLikes);
    }

    
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLikes([FromRoute] int id)
    {
        var existing = await _likes.GetById(id);
        if (existing is null)
            return NotFound("No likes found with given id");

        await _likes.Delete(id);

        return NoContent();
    }
}