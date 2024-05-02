using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reclytsites.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Sno { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string Issue { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string AssignedTo { get; set; }
        public string Status { get; set; }
    }
}
