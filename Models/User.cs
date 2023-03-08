using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OlxApi.Models{

    [Table("user")]
    public class User {

        [Key]
        [Required]
        [Column("_id")]
        public int _id { get; set; }
         
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
        [Column("state_id")]
        public int state_id { get; set; }
        public State state { get; set; }

        
    }
}