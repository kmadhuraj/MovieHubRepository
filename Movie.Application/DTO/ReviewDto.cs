using Movie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Application.DTO
{
    public class ReviewDto
    {
        public int UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public int MovieID { get; set; }
        [ForeignKey(nameof(MovieID))]
        public MovieModel Movie { get; set; }

        [Required(ErrorMessage ="rating field is required")]
        [Range(1.0,10.0,ErrorMessage ="Rating must be between 1 to 10 ")]
        public float Rating { get; set; }

        [Required(ErrorMessage ="Comment field is required")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [Range(typeof(DateTime), "2000-01-01", "2025-12-31", ErrorMessage = "Date must be between 01/01/2000 and 12/31/2025.")]
        public DateOnly Date { get; set; }
    }
}
