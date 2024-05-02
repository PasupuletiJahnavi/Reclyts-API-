namespace Reclytsites.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string AssignedTo { get; set; }
        public string Department { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string AppointmentDate { get; set; }
        public string TravelDistance { get; set; }
        public string HoursSpent { get; set; }
        public string RecallType { get; set; }
        public int NumberOfDevices { get; set; }
        public List<string> SerialNumbers { get; set; }
        public string ContactName { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
    }
}
