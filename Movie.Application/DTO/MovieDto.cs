using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.DTO
{
    public class MovieDto
    {

        [Required(ErrorMessage ="Title field is Required")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Release date field is required")]
        [DataType(DataType.Date)]
        public DateOnly ReleaseDate { get; set; }

        [Required(ErrorMessage = "Duration field is required")]
        [Range(1,1000)]
        public int Duration { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Director field is required")]
        public string Director { get; set; }

        
        [Required(ErrorMessage = "poster url field is required")]
        public string PosterURL { get; set; }
    }
}
