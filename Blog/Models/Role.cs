using MyBlog.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Role
    {

        
        public int Id { get; set; }
        public string RoleName { get; set; }
        

    }
}
