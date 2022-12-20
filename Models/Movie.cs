using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("How many in Stock")]
        [Range(1, 20, ErrorMessage = "Stock number must be between 1 and 20")]
        public int InStock { get; set; }
        public Genre Genre { get; set; }
        public byte GenreId { get; set; }

        public DateTime? ReleaseDate { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}
