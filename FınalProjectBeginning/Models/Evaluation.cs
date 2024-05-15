namespace FınalProjectBeginning.Models
{
    public class Evaluation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Like {  get; set; }

        public string? CetUserId { get; set; }
        public virtual CetUser? CetUser { get; set; }

        public int? EventId { get; set; }
        public virtual Event? Event { get; set; }
    }
}
