using System.ComponentModel.DataAnnotations;
using OlxApi.Models;

namespace OlxApi.Dtos{

    public class ReadImageDto{

        public string Url { get; set; }
        public bool Default { get; set; }
        public int ad_id { get; set; }
    }
}