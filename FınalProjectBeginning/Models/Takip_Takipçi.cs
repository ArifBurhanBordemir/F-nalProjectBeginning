using Microsoft.SqlServer.Management.Smo;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FınalProjectBeginning.Models
{
    public class Takip_Takipçi

    {
        public int Id { get; set; } 
        [ForeignKey("TakipEdenUser")]
        public string? TakipEdenUserId { get; set; }
        public virtual CetUser TakipEdenUser { get; set; }



        [ForeignKey("TakipEdilenKişi")]
        public string? TakipEdilenKişiId { get; set; }
        public virtual CetUser TakipEdilenKişi{ get; set; }


        
       
    }
}
