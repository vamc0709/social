using Social.Models;
using Dapper;
using Social.Utilities;


namespace Social.Repositories;

public interface IUserRepository
{
    Task<User> Create(User Item);
    Task<bool> Update(User Item);
    Task<bool> Delete(long user);
    Task<User> GetById(long user);
    Task<List<User>> GetList();



}
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<User> Create(User Item)
    {
        var query = $@"INSERT INTO ""{TableNames.users}"" 
        (user_name, first_name, last_name,  mobile, email) 
        VALUES (@UserName, @FirstName, @LastName, @Mobile, @Email) 
        RETURNING *";

        using (var con = NewConnection)
        {
            var res = await con.QuerySingleOrDefaultAsync<User>(query, Item);
            return res;



        }
    }

    public async Task<bool> Delete(long Id)
    {
        var query = $@"DELETE FROM ""{TableNames.users}"" 
        WHERE user_id= @Id";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { Id });
            return res > 0;
        }
    }

    public async Task<User> GetById(long Id)
    {
        var query = $@"SELECT * FROM ""{TableNames.users}"" 
        WHERE user_id = @Id";
        // SQL-Injection

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<User>(query, new { Id });
    }

    public async Task<List<User>> GetList()
    {
        // Query
        var query = $@"SELECT * FROM ""{TableNames.users}""";

        List<User> res;
        using (var con = NewConnection) // Open connection
            res = (await con.QueryAsync<User>(query)).AsList(); // Execute the query
        // Close the connection

        // Return the result
        return res;
    }

    public async Task<bool> Update(User Item)
    {
        var query = $@"UPDATE ""{TableNames.users}"" SET 
        first_name = @FirstName, last_name = @LastName, mobile = @Mobile,  email = @Email  WHERE  user_id = @UserId";


        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Item);
            return rowCount == 1;
        }
    }
}