using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class CreateUserDto{

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("passwordhash")]
        public string PasswordHash { get; set; }

        [MaxLength(50)]
        [Column("token")]
        public string Token { get; set; }


        [Required]
        [ForeignKey("state_id")]
        public int state_id { get; set; }        
    }
}