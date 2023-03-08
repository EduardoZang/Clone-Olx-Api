using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class CreateAdDto{

        [Required]
        [Column("title")]
        public string Title {get;set;}

        [Required]
        [Column("price")]
        public double Price {get;set;}

        [Required]
        [Column("priceNegotiable")]
        public bool PriceNegotiable {get;set;}

        [Required]
        [Column("description")]
        public string Description {get;set;}

        [Required]
        [Column("status")]
        public string Status {get;set;}

        [Required]
        [Column("views")]
        public int Views {get;set;}

        [Required]
        [Column("horaConsulta")]
        public DateTime HoraConsulta { get; set; } = DateTime.Now;



        [ForeignKey("state_id")]
        [Required]
        public int state_id { get; set; }

        [ForeignKey("category_id")]
        [Required]
        public int category_id { get; set; }

        [ForeignKey("user_id")]
        [Required]
        public int user_id { get; set; }
        
    }
}