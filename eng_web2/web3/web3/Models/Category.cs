using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace web3.Models
{
    public class Category
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)] se não queremos automático
        public int id { get; set; }

        [Required(ErrorMessage ="Required field")]
        [StringLength(50,MinimumLength =3, ErrorMessage ="{0} lenght must between")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Required field")]
        [MaxLength(256, ErrorMessage ="{0} lenght can not exceed {1} characteres")]
        public string Description { get; set; }

        public Boolean State { get; set; } = true;

        [DisplayName("Creation Date")]
        public DateTime Date {  get; set; } = DateTime.Now;
    }
}
