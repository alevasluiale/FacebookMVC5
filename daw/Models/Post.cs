using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace daw.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public String UserId { get; set; }
        [Required]
        [DisplayName("Date of post")]
        public DateTime DateOfPost { get; set; }
        public String UserName { get; set; }
        [DisplayName("Text Content")]
        public String TextContent { get; set; }

        //public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        //public List<Image> Images { get; set; }
        //public List<Comment> Comments { get; set; }
        //public List<Like> Likes { get; set; }
    }
}