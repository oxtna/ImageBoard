using System;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Timestamp")]
        public DateTime Timestamp { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 1)]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public static int CompareByDates(Post x, Post y)
        {
            return x.Timestamp.CompareTo(y.Timestamp);
        }
    }
}
