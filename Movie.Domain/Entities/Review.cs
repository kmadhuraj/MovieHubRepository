using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public int MovieID { get; set; }
        [ForeignKey(nameof(MovieID))]
        public MovieModel Movie { get; set; }

        public float Rating { get; set; }

        public string Comment { get; set; }

       
        public DateOnly Date { get; set; }

    }
}
