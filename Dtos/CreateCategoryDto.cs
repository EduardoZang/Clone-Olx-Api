using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class CreateCategoryDto{

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(15)]
        [Column("slug")]
        public string Slug { get; set; }
    }
}