using Blog.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlog.Models

{
    public class User : IdentityUser
    {      
        
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        public DateTime BirthDate { get; set; }
       

    }
}
