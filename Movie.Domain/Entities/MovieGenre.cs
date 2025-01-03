using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class MovieGenre
    {
        [Key]
        public int Id { get; set; }
        public int GenreID { get; set; }
        [ForeignKey(nameof(GenreID))]
        public Genre Genre { get; set; }

        public int MovieID { get; set; }
        [ForeignKey(nameof(MovieID))]
        public MovieModel Movie { get; set;}

    }
}
        