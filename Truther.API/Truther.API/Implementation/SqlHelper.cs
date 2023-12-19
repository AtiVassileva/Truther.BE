using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
