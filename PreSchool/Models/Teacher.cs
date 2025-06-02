using PreSchool.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace PreSchool.Models
{
    public class Teacher:BaseEntity
    {
        public string Name { get; set; }
        public string Position { get; set; }   
        public string? imageUrl { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

        //public int anarAge { get; set; }
    }
}
