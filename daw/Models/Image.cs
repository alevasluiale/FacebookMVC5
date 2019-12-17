using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace daw.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public String Path { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }

        
        public virtual Post post { get; set; }
        //public virtual Comment comment { get; set; }
    }

}