using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Truther.API.Common;
using Truther.API.Models;

namespace Truther.API.Implementation;
public class SqlHelper
{
    private readonly string _connectionString;

    public SqlHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Post[] GetPosts()
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
                            Id = Convert.ToInt32(reader["Id"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString()
                        };
                        posts.Add(post);
                    }
                }
            }
        }

        return posts.ToArray();
    }

    public Post GetPost(Guid postId)
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
                            Id = Convert.ToInt32(reader["Id"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Title = reader["Title"].ToString(),
                            Content = reader["Content"].ToString()
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
                command.Parameters.AddWithValue("@user_guid", post.UserId);
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