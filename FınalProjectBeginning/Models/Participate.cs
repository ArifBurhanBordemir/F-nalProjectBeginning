using Azure;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FınalProjectBeginning.Models
{
    public class Participate
    {
        public int Id { get; set; }


        public string? CetUserId { get; set; }
        public  virtual CetUser? CetUser { get; set; } 


        public int EventId { get; set; }
        public  virtual Event? Event { get; set; } 

    }
}
