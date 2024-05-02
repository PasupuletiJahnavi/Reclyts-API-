namespace Reclytsites.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Instructions { get; set; }
        public string Author { get; set; }
        public string ProfileImage { get; set; }
        public DateTime Date { get; set; }
        public string ? Section { get; set; }
    }
}
