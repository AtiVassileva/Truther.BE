using System;
using System.Collections.Generic;

namespace Truther.API.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int? UserId { get; set; }
        public int? PostId { get; set; }

        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
