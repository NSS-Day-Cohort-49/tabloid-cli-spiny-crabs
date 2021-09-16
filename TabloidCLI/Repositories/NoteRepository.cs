using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    class NoteRepository : DatabaseConnector
    {
        public NoteRepository(string connectionString) : base(connectionString) {  }
        public List<Note> GetPostNotes(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT * FROM Note WHERE PostId = @id";
                    cmd.Parameters.AddWithValue("@id", postId);

                    List<Note> notes = new List<Note>();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        notes.Add(note);
                    }
                    return notes;
                }
            }
        }
    }
}
