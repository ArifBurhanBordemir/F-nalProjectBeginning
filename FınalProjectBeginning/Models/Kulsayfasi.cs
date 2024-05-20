namespace FınalProjectBeginning.Models
{
    public class Kulsayfasi
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }

        public string? CetUserId { get; set; }
        public virtual CetUser? CetUser { get; set; }

    }
}
