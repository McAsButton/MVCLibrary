using System.ComponentModel.DataAnnotations;

namespace Library.Web.Data.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Titulo")]
        public string Title { get; set; } = null!;

        [MaxLength(128, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripción")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public DateTime PublishDate { get; set; }

        public Author Author { get; set; } = null!;

        public int AuthorId { get; set; }

        [MaxLength(32, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Editorial")]
        public string Editorial { get; set; } = null!;
    }
}
