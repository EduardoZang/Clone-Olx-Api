using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlxApi.Models{

    [Table("state")]
    public class State {

        [Key]
        [Required]
        [Column("_id")]
        public int _id { get; set; }
         
        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }
    }
}