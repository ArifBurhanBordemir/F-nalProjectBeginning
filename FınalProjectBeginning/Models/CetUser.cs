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




            //Navigation Properties
            public List<Participate> Participates { get; set; }
            public virtual ICollection<Takip_Takipci> TakipEdenUsers { get; set; }
            public virtual ICollection<Takip_Takipci> TakipEdilenKisis { get; set; }

      

            public virtual List<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
            public virtual List<Post> Posts { get; set; } = new List<Post>();
            public virtual List<Kulsayfasi> Kulsayfasis { get; set; } = new List<Kulsayfasi>();










    }

}

