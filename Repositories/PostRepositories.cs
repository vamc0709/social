using Dapper;
using Social.DTOs;
using Social.Models;
using Social.Repositories;
using Social.Utilities;

public interface IPostRepository
{
    Task<Post> Create(Post Item);
    //Task Update(Post Item);
    Task Delete(long Id);
    Task<List<PostDTO>> GetAllForUser(long userId);
    Task<List<Post>> GetList();
    Task<List<PostDTO>>GetAllForHashtag(long PostId );
    // Task<List<PostDTO>>GetAllLikes(long LikeId );

    Task<Post> GetById(long Id);
    // Task GetById(long postId);
}

public class PostRepository : BaseRepository, IPostRepository
{
    public PostRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Post> Create(Post Item)
    {
        var query = $@"INSERT INTO {TableNames.posts} (post_id,user_id,no_of_images) VALUES (@PostId, @UserId, @NoOfImages) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Post>(query, Item);
    }



    public async Task Delete(long Id)
    {
        var query = $@"DELETE FROM {TableNames.posts} WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    } 

   

    public async Task<List<PostDTO>> GetAllForUser(long UserId)
    {
        var query = $@"SELECT * FROM {TableNames.posts} WHERE user_id = @UserId";

        using (var con = NewConnection)
              return (await con.QueryAsync<PostDTO>(query, new{UserId})).AsList();
    }

    // public async Task<Post> GetById(int PostId)
    // {
        
    // }

    public async Task<List<Post>> GetList()
    {
        var query = $@"SELECT * FROM ""{TableNames.posts}""";

        List<Post> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<Post>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public async Task<List<PostDTO>> GetAllForHashtag(long HashId)
    {
        var query = $@"SELECT  * FROM {TableNames.hashpost} hp
        LEFT JOIN {TableNames.posts} p ON p.post_id = hp.post_id
        WHERE hash_id = @HashId";

         using (var con = NewConnection) // Open connection
            return(await con.QueryAsync<PostDTO>(query,new {HashId})).AsList();
    }

    public async Task<Post> GetById(long Id)
    {
          var query = $@"SELECT * FROM {TableNames.posts} 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Post>(query, new {Id});
    }

    // public async Task<List<postDTO>> GetAllPosts(long LikeId)
    //  {
    //      var query = $@"SELECT * FROM {TableNames.posts} 
    //      WHERE like_id = @LikeId";

    //      using (var con = NewConnection)
    //          return (await con.QueryAsync<postDTO>(query, new { LikeId })).AsList();
    // }
}

    


