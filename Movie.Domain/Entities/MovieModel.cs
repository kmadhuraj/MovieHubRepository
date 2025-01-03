using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class MovieModel
    {
        [Key]
        public int MovieID { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int Duration { get; set; }

        [MaxLength(50)]
        public string Director { get; set; }

        
        public string PosterURL { get; set; }


    }
}
