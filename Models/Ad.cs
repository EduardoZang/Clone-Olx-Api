using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlxApi.Models{

     [Table("ad")]
    public class Ad
    {
        [Key]
        [Required]
        [Column("_id")]
        public int _id {get;set;}

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


        [Required]
        [Column("state_id")]
        public int state_id { get; set; }
        public State state { get; set; }

        [Required]
        [Column("category_id")]
        public int category_id { get; set; }
        public Category category { get; set; }

        [Required]
        [Column("user_id")]
        public int user_id { get; set; }
        public User user { get; set; }
    }
}