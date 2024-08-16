using Microsoft.Data.SqlClient;
using System;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.Repositories
{
    internal class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, 
                                               Title, 
                                               Content, 
                                               CreateDateTime 
                                       FROM Note";

                    List<Note> notes = new List<Note> ();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) 
                    {
                        Note note = new Note();
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),

                        };
                        notes.Add(note);
                    }
                    reader.Close();
                    return notes;
                }
            }

        }
        public Note Get(int id) 
        {
            using (SqlConnection conn = Connection) 
            {
                conn.Open ();
                using (SqlCommand cmd = conn.CreateCommand()) 
                {
                    cmd.CommandText = @"SELECT n.Id AS NoteId,
                                               n.Title,
                                               n.Content,
                                               n.CreateDateTime
                                               p.Title
                                        FROM Note n
                                        LEFT JOIN ";
                }
            }
        }
    }
}
