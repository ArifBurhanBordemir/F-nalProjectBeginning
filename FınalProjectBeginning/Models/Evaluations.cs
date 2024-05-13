namespace FınalProjectBeginning.Models
{
    public class Evaluations
    {
        public int Id { get; set; }
        public string Yorum { get; set; }
        public bool Like { get; set;}

        public string? CetUserId { get; set; }
        public virtual CetUser? CetUser { get; set; }


        public int EventId { get; set; }
        public virtual Event? Event { get; set; }
    }
}
