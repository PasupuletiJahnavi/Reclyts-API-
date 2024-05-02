using System.ComponentModel.DataAnnotations;

namespace Reclytsites.Models
{
    public class Tutorial
    {
        [Key]
        public int Id { get; set; }
        public string ? Description { get; set; }
        public string[] ? EssenceOfAssessment { get; set; }
        public string ImagePath { get; set; }
    }

}
