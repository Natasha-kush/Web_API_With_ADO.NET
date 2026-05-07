using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CrudAppUsingWebAPI.Models
{

 public class Student
     {
        //properties set
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string studentName { get; set; }

        [Required]
        public string studentGender { get; set; }

        [Required]
        public int age { get; set; }

        [Required]
        public int standard { get; set; }

        [Required]
        public string fatherName { get; set; }
     }

    
}
