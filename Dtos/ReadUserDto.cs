using System.ComponentModel.DataAnnotations;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class ReadUserDto{

        public string Name { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Token { get; set; }

        public string state_id { get; set; }
        
    }
}