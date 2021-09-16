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

        public void Insert(Note note, int _postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId)
                                        VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", _postId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Note 
                                        WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
