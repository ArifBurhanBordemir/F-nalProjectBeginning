namespace FınalProjectBeginning.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }


        public string? ImageName { get; set; }


        public string? CetUserId { get; set; }
        public virtual CetUser? CetUser { get; set; }
    }
}
