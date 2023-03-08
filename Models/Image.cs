using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlxApi.Models{

    [Table("image")]
    public class Image {

        [Key]
        [Required]
        [Column("_id")]
        public int _id { get; set; }
         
        [Required]
        [Column("url")]
        public string Url { get; set; }

        [Required]
        [Column("default")]
        public bool Default { get; set; }

        [Required]
        [Column("ad_id")]
        public int ad_id { get; set; }
        public Ad ad { get; set; }
    }
}