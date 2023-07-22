using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviezLand.Core.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        [Required]
        [MaxLength(2500)]
        public string Story { get; set; }
        [Required]
        public byte[] Poster { get; set; }
        [Required]
        public int Year { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
    }
}
