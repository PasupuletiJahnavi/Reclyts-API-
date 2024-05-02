namespace Reclytsites.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssignedTo { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }
}
