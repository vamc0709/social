using Dapper;
using social.Models;
using Social.DTOs;
using Social.Utilities;

namespace Social.Repositories;

public interface IHashtagRepository
{
    Task<Hashtag> Create(Hashtag Item);
    Task<bool> Update(Hashtag Item);
    Task<bool> Delete(long Id);
    Task<List<Hashtag>>GetList();
    Task<Hashtag> GetById(long Id);
    Task<List<HashtagDTO>> GetAllForPost(long PostId);
    

}

public class HashtagRepository : BaseRepository, IHashtagRepository
{
    public HashtagRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Hashtag> Create(Hashtag Item)
    {
        var query = $@"INSERT INTO ""{TableNames.hash}"" 
        (hash_name) VALUES (@HashName) RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<Hashtag>(query, Item);
            return res;
        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.hash}"" 
        WHERE hash_id = Hash@Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

   

    public async Task<List<HashtagDTO>> GetAllForPost(long PostId)
    {
        var query = $@"SELECT * FROM {TableNames.hash} 
        WHERE post_id = @PostId";

        using (var con = NewConnection)
            return (await con.QueryAsync<HashtagDTO>(query, new {PostId})).AsList();
    }

    

    public async Task<Hashtag> GetById(long PostId)
    {
        var query = $@"SELECT * FROM ""{TableNames.hash}"" 
        WHERE hash_id = @PostId";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Hashtag>(query, new {PostId });
    }

    public async Task<List<Hashtag>> GetList()
    {
         var query = $@"SELECT * FROM ""{TableNames.hash}""";

        List<Hashtag> res;
        using (var con = NewConnection)
            res = (await con.QueryAsync<Hashtag>(query)).AsList();
        return res;
    }

    public async Task<bool> Update(Hashtag Item)
     {
         var query = $@"UPDATE ""{TableNames.hash}"" SET  name = @Name WHERE hash_id = @Id";
         

         using (var con = NewConnection)
         {
             var rowCount = await con.ExecuteAsync(query, Item);
             return rowCount == 1;
         }
     }

   
   
}

    
