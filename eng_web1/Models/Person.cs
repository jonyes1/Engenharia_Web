using System.ComponentModel.DataAnnotations;


namespace eng_web1.Models

{
    public class Person
    {

        [Required(ErrorMessage ="Mandatory field")]
        [Display(Name="Nome do utilizador")]
        [DataType(DataType.Password)]
        //[StringLength(20,ErrorMessage = "jhtjghyt")]

        public string? Name { get; set; }


        [Required(ErrorMessage = "The field {0} is mandatory")]
        [Range(18, 100, ErrorMessage = "{0} must be between {1} and {2}")]

        public int Age { get; set; }


        [DataType(DataType.Date)]

        public DateTime Birthday { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
    }
}

