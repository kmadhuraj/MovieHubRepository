using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.DTO
{
    public class GenreDto
    {
        [MaxLength(50)]
        [Required(ErrorMessage ="Name field is required ")]
        public string Name { get; set; }
    }
}
