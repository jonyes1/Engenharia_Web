using System.ComponentModel.DataAnnotations;

namespace Aula5.Models
{
    public class BookViewModel
    {

        [Required(ErrorMessage = "Required Field")]
        [StringLength(100, ErrorMessage = "The {0} do not exceed {1} characteres.")]
        public string? Title { get; set; }
        [Required]
        public IFormFile? CoverPhoto { get; set; }
        [Required]
        public IFormFile? Document { get; set; }
    }
}
