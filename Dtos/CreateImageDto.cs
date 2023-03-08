using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class CreateImageDto{

        [Required]
        [Column("url")]
        public string Url { get; set; }

        [Required]
        [Column("default")]
        public bool Default { get; set; }

        [Required]
        [ForeignKey("ad_id")]
        public int ad_id { get; set; }        
    }
}