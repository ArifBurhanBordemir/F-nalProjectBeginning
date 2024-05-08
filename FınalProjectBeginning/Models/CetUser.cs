using Microsoft.AspNetCore.Identity;

namespace FınalProjectBeginning.Models
{
    public class CetUser : IdentityUser
    {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime BirthDate { get; set; }
            public virtual List<Event> Events { get; set; } = new List<Event>();


            //public virtual List<Takip_Takipçi> TakipEdenUserlar { get; set; }
            //public virtual List<Takip_Takipçi> TakipEdilenKişiler { get; set; }


    }

}

