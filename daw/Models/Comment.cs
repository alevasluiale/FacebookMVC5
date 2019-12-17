using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace daw.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public String UserId { get; set; }
        public String UserName { get; set; }
        public String TextContent { get; set; }


        public virtual Post post { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }

  
}