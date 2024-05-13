using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace FınalProjectBeginning.Models
{
    public class CetUser : IdentityUser
    {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public virtual List<Event> Events { get; set; } = new List<Event>();


        //public virtual ICollection<Participate> Participates { get; set; } = new List<Participate>();


        //public virtual List<Takip_Takipçi> TakipEdenUserlar { get; set; }
        //public virtual List<Takip_Takipçi> TakipEdilenKişiler { get; set; }

        //Navigation Properties
        public List<Participate> Participates { get; set; }
        public virtual ICollection<Takip_Takipçi> TakipEdenUsers { get; set; }
        public virtual ICollection<Takip_Takipçi> TakipEdilenKişis { get; set; }





    }

}

