using System;
using System.Collections.Generic;


namespace TabloidCLI.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDateTime { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
