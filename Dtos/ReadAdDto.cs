using System.ComponentModel.DataAnnotations;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class ReadAdDto{

        public int user_id {get;set;}
        public string Title {get;set;}
        public double Price {get;set;}
        public bool PriceNegotiable {get;set;}
        public string Description {get;set;}
        public string Status {get;set;}
        public string state_id { get; set; }
        public string category_id { get; set; }
        public int Views {get;set;}
        public DateTime HoraConsulta { get; set; } = DateTime.Now;
    }
}