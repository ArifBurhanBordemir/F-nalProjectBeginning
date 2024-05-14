namespace FınalProjectBeginning.Models
{
    public class Takip_Takipci
    {
        public int Id { get; set; }

        public string? TakipEdilenKisiId { get; set; }
        public virtual CetUser? TakipEdilenKisi { get; set; }

        public string? TakipEdenUserId { get; set; }
        public virtual CetUser? TakipEdenUser { get; set; }


    }
}
