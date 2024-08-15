using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id,
                                               Title,
                                               URL
                                          FROM Blog";
                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url")),
                        };
                        blogs.Add(blog);

                    }
                    return blogs;
                }
            }
            }

            public Blog Get(int id)
            {
                throw new NotImplementedException();
            }

            public void Insert(Blog blog)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO Blog (Title, URL)
                                        VALUES (@title, @URL)";

                        //cmd.Parameters.AddWithValue("@id", blog.Id); (Leaving id here and adding it to the CommnadText will throw an error)
                        cmd.Parameters.AddWithValue("@title", blog.Title);
                        cmd.Parameters.AddWithValue("@url", blog.Url);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void Update(Blog blog)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE Blog
                                            SET Title = @title
                                            WHERE id = @id";

                        cmd.Parameters.AddWithValue("@title", blog.Title);
                        cmd.Parameters.AddWithValue("@id", blog.Id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public void Delete(int id)
            {
                using (SqlConnection connection = Connection)
                {

                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"DELETE FROM Blog Where id = @id";
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();
                    }

                }

            }
        }
    }