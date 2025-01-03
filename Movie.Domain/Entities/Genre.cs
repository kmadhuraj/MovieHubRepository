using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie.Domain.Entities
{
    public class Genre
    {
        [Key]
        public int GenreID { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }



    }
}
