using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;

namespace FınalProjectBeginning.Models
{
    public class Event
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Body { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.Now;
            public DateTime EventDate { get; set; } 
            public string EventLocation { get; set; }


            public string? ImageName { get; set; }


            public string? CetUserId { get; set; }
            public virtual CetUser? CetUser { get; set; }

            public int? ReadCount { get; set; } = 0;


            public virtual List<Menu> Menus { get; set; } = new List<Menu>();



            //public virtual ICollection<Participate> Participates { get; set; } = new List<Participate>();

            //public List<CetUser> CetUsers { get; } = [];
            //Navigation Properties
            public List<Participate> Participates { get; set; }

    }

}

