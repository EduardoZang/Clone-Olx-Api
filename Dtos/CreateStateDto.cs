using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class CreateStateDto{

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }
    }
}