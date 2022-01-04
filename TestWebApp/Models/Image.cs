using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
    }
}
