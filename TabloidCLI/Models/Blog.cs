using System.Collections.Generic;

namespace TabloidCLI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();

        public List<Post> Posts { get; set; } = new List<Post>();

        public override string ToString()
        {
            return $"{Title} ({Url})";
        }
    }
}