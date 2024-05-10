using System.ComponentModel.DataAnnotations.Schema;

namespace FınalProjectBeginning.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }


        

        public int? EventId { get; set; }
        public virtual Event? Event { get; set; }
    }
}
