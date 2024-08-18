namespace CoreBlogWebApp.Models
{
    public class AddProfilePicture
    {
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? AuthorAbout { get; set; }
        public string? AuthorMail { get; set; }
        public string? AuthorPassword { get; set; }
        public IFormFile? AuthorImg { get; set; }
        public bool? AuthorStatus { get; set; }
    }
}
