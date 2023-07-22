using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.Models
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(2500)]
       public string Content { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
