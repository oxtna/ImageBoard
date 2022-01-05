using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestWebApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
