using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Truther.API.Common;
using Truther.API.Models;

namespace Truther.API.Implementation
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Post> GetPosts()
        {
            var posts = new List<Post>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
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

            return posts;
        }

        public void LikePost(Guid postId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE LIKES SET count = count + 1 WHERE postId = @PostId", connection))
                {
                    command.Parameters.AddWithValue("@PostId", postId);

                    using (var rowsAffected = command.ExecuteNonQueryAsync())
                    {
                        if (rowsAffected != 1)
                        {
                            throw new Exception(AlertMessages.ErrorOnLike);
                        }
                    }
                }
            }
        }
    }

    public void CreatePost(Post post)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand("INSERT INTO Posts (userId, title, content) VALUES (@user_guid, @title, @content)", connection))
            {
                command.Parameters.AddWithValue("@user_guid", post.UserId);
                command.Parameters.AddWithValue("@title", post.Title);
                command.Parameters.AddWithValue("@content", post.Content);

                using (var rowsAffected = command.ExecuteNonQueryAsync())
                {
                    if (rowsAffected != 1)
                    {
                        throw new Exception(AlertMessages.ErrorOnLike);
                    }
                }
            }
        }
    }
}