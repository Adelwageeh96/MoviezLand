using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required,MaxLength(50),MinLength(3)]
        public string FirstName { get; set; }

        [Required,MaxLength(50),MinLength(3)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        public byte[]? ProfilePicture { get; set; }
    }
}
