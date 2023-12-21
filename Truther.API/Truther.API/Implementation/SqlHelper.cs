using System.Data.SqlClient;
using Truther.API.Common;
using Truther.API.Infrastructure;
using Truther.API.Models;

namespace Truther.API.Implementation;
public class SqlHelper
{
    private readonly string _connectionString;
    private readonly UserExtensions _userExtensions = new(new TrutherContext(), new HttpContextAccessor());

    public SqlHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Post[]> GetPostsAsync()
    {
        var posts = new List<Post>();

        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("SELECT * FROM Posts", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var post = new Post
                        {
                            Id = new Guid(),
                            UserId = await _userExtensions.GetCurrentUserId(),
                            Title = reader["Title"].ToString()!,
                            Content = reader["Content"].ToString()!
                        };
                        posts.Add(post);
                    }
                }
            }
        }

        return posts.ToArray();
    }

    public async Task<Post> GetPost(Guid postId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();

            using (var command = new SqlCommand("SELECT * FROM Posts WHERE [Id] = @Id", connection))
            {
                command.Parameters.AddWithValue("@Id", postId);

                using (var reader = command.ExecuteReader())
                {
                   if(reader.Read())
                    {
                        var post = new Post
                        {
                            Id = new Guid(),
                            UserId = await _userExtensions.GetCurrentUserId(),
                            Title = reader["Title"].ToString()!,
                            Content = reader["Content"].ToString()!
                        };

                        return post;
                    }

                    throw new Exception(AlertMessages.NonExistingPost);
                }
            }
        }

       
    }

    public async Task LikePost(Guid postId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("UPDATE LIKES SET count = count + 1 WHERE postId = @PostId", connection))
            {
                command.Parameters.AddWithValue("@PostId", postId);

                var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                if (rowsAffected != 1)
                {
                    throw new Exception(AlertMessages.ErrorOnLike);
                }

            }
        }
    }
    
    public async Task CreatePost(Post post)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("INSERT INTO Posts (userId, title, content) VALUES (@user_guid, @title, @content)", connection))
            {
                var currentUsedId = await _userExtensions.GetCurrentUserId();
                command.Parameters.AddWithValue("@user_guid", currentUsedId);
                command.Parameters.AddWithValue("@title", post.Title);
                command.Parameters.AddWithValue("@content", post.Content);

                var rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);

                if (rowsAffected != 1)
                {
                    throw new Exception(AlertMessages.ErrorOnLike);
                }
            }
        }
    }
}