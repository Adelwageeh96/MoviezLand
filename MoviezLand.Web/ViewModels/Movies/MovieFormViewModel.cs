using MoviezLand.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MoviezLand.Web.ViewModels.Movies
{
    public class MovieFormViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [StringLength(2500)]
        public string Story { get; set; }

        public byte[]? Poster { get; set; }

        [Required]
        public int Year { get; set; }

        [DisplayName("Genres")]
        public List<int> SelectedGenreIds { get; set; } // Store selected genre IDs

        public IEnumerable<Genre>? Genres { get; set; } 
    }
}
