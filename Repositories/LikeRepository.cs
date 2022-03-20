


using Dapper;
using Social.DTOs;
using Social.Models;
using Social.Utilities;

namespace Social.Repositories;

public interface ILikesRepository
{
    
    Task<Likes> Create(Likes Item);
    Task Delete(long Id);
    Task<List<LikesDTO>> GetAllForPost(long PostId);
    Task<List<Likes>>GetList();
    
    Task<Likes> GetById(long Id);
    
}

public class LikesRepository : BaseRepository, ILikesRepository
{
    public LikesRepository(IConfiguration config) : base(config)
    {

    }
    public async Task<Likes> Create(Likes Item)
    {
        var query = $@"INSERT INTO {TableNames.likes} (created_at,post_id,user_id) VALUES (@CreatedAt, @PostId, @UserId) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Likes>(query, Item);
    }



    public async Task Delete(long Id)
    {
        var query = $@"DELETE FROM {TableNames.likes} WHERE user_id= @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    }


    public async Task<List<LikesDTO>> GetAllForPost(long PostId)
    {
        var query = $@"SELECT * FROM {TableNames.likes} 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return (await con.QueryAsync<LikesDTO>(query, new { PostId })).AsList();
    }

    public async Task<Likes> GetById(long Id)
    {
         var query = $@"SELECT * FROM {TableNames.likes} 
        WHERE like_id = @Id";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Likes>(query, new { Id });
    }

    public async Task<List<Likes>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.likes}""";

        List<Likes> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Likes>(query)).AsList();
        return res;
    }
}